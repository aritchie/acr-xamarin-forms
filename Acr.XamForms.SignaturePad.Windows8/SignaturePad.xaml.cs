using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Windows.Storage.Streams;
using Acr.XamForms.SignaturePad;
using System.Diagnostics;

namespace Xamarin.Controls
{
    public partial class SignaturePad : UserControl
    {
        Point previousPosition = default(Point);
        List<List<Point>> points = new List<List<Point>>();
        bool pressed = false;
        BitmapInfo bitmapInfo = new BitmapInfo();

        //Create an array containing all of the points used to draw the signature.  Uses null
        //to indicate a new line.
        public Point[] Points
        {
            get
            {
                if (points == null || points.Count == 0)
                    return new Point[0];

                IEnumerable<Point> pointsList = points[0];

                for (var i = 1; i < points.Count; i++)
                {
                    pointsList = pointsList.Concat(new[] { new Point() });
                    pointsList = pointsList.Concat(points[i]);
                }

                return pointsList.ToArray();
            }
        }

        public bool IsBlank
        {
            get { return points.Count () == 0; }
        }

        /// <summary>
        /// Retrieves the signature image from the canvas
        /// </summary>
        /// <param name="imgFormat"></param>
        /// <returns></returns>
        public Stream GetImage(ImageFormatType imgFormat)
        {
            var imageTask = Task.Run(async () =>
            {
                //Create new Random Access Tream
                var stream = new InMemoryRandomAccessStream();

                //Create bitmap encoder
                var encoder = await BitmapEncoder.CreateAsync(
                    imgFormat == ImageFormatType.Png ? BitmapEncoder.PngEncoderId : BitmapEncoder.JpegEncoderId, stream);

                //Set the pixel data and flush it
                encoder.SetPixelData(
                            BitmapPixelFormat.Bgra8,
                            BitmapAlphaMode.Straight,
                            (uint)bitmapInfo.PixelWidth,
                            (uint)bitmapInfo.PixelHeight, 96d, 96d,
                            bitmapInfo.BitmapBuffer.ToArray());
                await encoder.FlushAsync();
                return stream.AsStream();
            });
            imageTask.Wait();
            return imageTask.Result;
        }

        /// <summary>
        /// Updates the BitmapBuffer object with the current state of the canvas
        /// </summary>
        public void UpdateBitmapBuffer()
        {
            Task.Run(() =>
            {
                var task = this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    Size canvasSize = this.inkPresenter.RenderSize;
                    Point defaultPoint = this.inkPresenter.RenderTransformOrigin;

                    this.inkPresenter.Measure(canvasSize);
                    this.inkPresenter.UpdateLayout();
                    this.inkPresenter.Arrange(new Rect(defaultPoint, canvasSize));

                    // Convert canvas to bmp.  
                    var renderTargetBitmap = new RenderTargetBitmap();
                    await renderTargetBitmap.RenderAsync(this.inkPresenter);

                    //Fetch the Pixel Buffer fromt the bitmap
                    bitmapInfo.BitmapBuffer = await renderTargetBitmap.GetPixelsAsync();
                    bitmapInfo.PixelWidth = renderTargetBitmap.PixelWidth;
                    bitmapInfo.PixelHeight = renderTargetBitmap.PixelHeight;
                }).AsTask();
                task.Wait();
            });
        }

        /// <summary>
		/// Gets or sets the color of the strokes for the signature.
		/// </summary>
		/// <value>The color of the stroke.</value>
        public SolidColorBrush Stroke
        {
            get; set;
        }

        Color backgroundColor;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { 
                backgroundColor = value;
                LayoutRoot.Background = new SolidColorBrush (value);
            }
        }

        /// <summary>
        /// Gets or sets the width in pixels of the strokes for the signature.
        /// </summary>
        /// <value>The width of the line.</value>
        public int StrokeWidth
        {
            get { return (int)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        public static readonly DependencyProperty StrokeWidthProperty =
            DependencyProperty.Register("StrokeWidth", typeof(int), typeof(SignaturePad), new PropertyMetadata(2));

        /// <summary>
		/// The prompt displayed at the beginning of the signature line.
		/// </summary>
		/// <remarks>
		/// Text value defaults to 'X'.
		/// </remarks>
		/// <value>The signature prompt.</value>
        public TextBlock SignaturePrompt {
            get { return signaturePrompt; }
        }

        /// <summary>
        /// The caption displayed under the signature line.
        /// </summary>
        /// <remarks>
        /// Text value defaults to 'Sign here.'
        /// </remarks>
        /// <value>The caption.</value>
        public TextBlock Caption
        {
            get { return caption; }
        }

        public TextBlock ClearLabel
        {
            get { return clearText; }
        }

        /// <summary>
		/// The color of the signature line.
		/// </summary>
		/// <value>The color of the signature line.</value>
		protected Color signatureLineColor;
        public Color SignatureLineColor
        {
            get { return signatureLineColor; }
            set
            {
                signatureLineColor = value;
                signatureLine.BorderBrush = new SolidColorBrush(value);
            }
        }

        public SignaturePad ()
        {
            InitializeComponent ();
        }
        /// <summary>
        /// Fires when SignatureBox has been loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inkPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            var background = base.Background ?? new SolidColorBrush(Colors.White);
            this.Background = background;
        }
        //Delete the current signature
        public void Clear ()
        {
            var lines = new List<Line>();

            foreach (var child in this.inkPresenter.Children)
            {
                if (child.GetType() == typeof(Line))
                {
                    lines.Add(child as Line);
                }
            }

            foreach (var line in lines)
                inkPresenter.Children.Remove(line);

            points = new List<List<Point>>(); //Reset Points

            clearText.Visibility = Visibility.Collapsed;
        }

        private void btnClear_Click (object sender, PointerRoutedEventArgs e)
        {
            Clear ();
        }

        #region Touch Events
        /// <summary>
        /// Handles the pointer pressed event. Sets the previousPosition to the 
        /// current position of the pointer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void inkPresenter_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // Get information about the pointer location. 
            previousPosition = e.GetCurrentPoint(this).Position;
            pressed = true;
        }

        /// <summary>
        /// Handles the pointer moved event. If the pointer is pressed and has moved it will 
        /// draw the line on the canvas and add the points drawn to the points list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void inkPresenter_OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (!pressed) return;

            var positions = e.GetIntermediatePoints(this).Select(ppt => ppt.Position);
            var currentPosition = e.GetCurrentPoint(this).Position;

            //Only add points if previous and current positions are different
            if (positions.Any() &&
                currentPosition.X != previousPosition.X &&
                currentPosition.Y != previousPosition.Y)
            {
                points.Add(new List<Point>());
                points.Last().Add(previousPosition);

                foreach (Point pt in positions)
                {
                    this.inkPresenter.Children.Add(
                      new Line()
                      {
                          X1 = previousPosition.X,
                          Y1 = previousPosition.Y,
                          X2 = pt.X,
                          Y2 = pt.Y,
                          Stroke = this.Stroke,
                          StrokeThickness = this.StrokeWidth
                      }
                    );
                    previousPosition = pt;
                }
                points.Last().AddRange(positions);
            } 
        }

        /// <summary>
        /// Handles the PointerReleased Event. Toggled the Visibility of the clearText TextBlock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void inkPresenter_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (pressed)
                pressed = false;

            if (inkPresenter.Children.Any())
            {
                clearText.Visibility = Visibility.Visible;
            }

            UpdateBitmapBuffer(); //Update the BitMapBuffer model with the current state of the canvas
        }
        #endregion

        //Allow the user to import an array of points to be used to draw a signature in the view, with new
        //lines indicated by a PointF.Empty in the array.
        public void LoadPoints (Point [] loadedPoints)
        {
            if (!loadedPoints.Any())
            {
                return;
            }
  
            //Clear any existing paths or points.
            points = new List<List<Point>>();

            foreach (Point pt in loadedPoints)
            {
                this.inkPresenter.Children.Add(
                  new Line()
                  {
                      X1 = previousPosition.X,
                      Y1 = previousPosition.Y,
                      X2 = pt.X,
                      Y2 = pt.Y,
                      Stroke = this.Stroke,
                      StrokeThickness = this.StrokeWidth
                  }
                );
                previousPosition = pt;
            }

            points.Last().AddRange(loadedPoints);
            ////Obtain the image for the imported signature and display it in the image view.
            //image.Source = GetImage (false);
            ////Display the clear button.
            clearText.Visibility = Visibility.Visible;

            UpdateBitmapBuffer(); //Update the BitmapBuffer
        }

        /// <summary>
        /// Class to contain the Current State of the Canvas Bitmap
        /// </summary>
        private class BitmapInfo
        {
            public IBuffer BitmapBuffer
            {
                get; set;
            }
            public int PixelWidth
            {
                get; set;
            }
            public int PixelHeight
            {
                get; set;
            }
        }
    }
}