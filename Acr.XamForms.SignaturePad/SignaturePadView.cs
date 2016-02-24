﻿using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;


namespace Acr.XamForms.SignaturePad {

    public class SignaturePadView : View {
        private Func<ImageFormatType, Stream> getImageFunc;
        private Func<IEnumerable<DrawPoint>> getDrawPointsFunc;
        private Action<IEnumerable<DrawPoint>> loadDrawPoints;
        private Func<bool> isBlankFunc;
        private Action clearFunc;


        public Stream GetImage(ImageFormatType imageFormat) {
            return this.getImageFunc(imageFormat);
        }


        public IEnumerable<DrawPoint> GetDrawPoints() {
            return this.getDrawPointsFunc();
        }


        public void LoadDrawPoints(IEnumerable<DrawPoint> drawPoints) {
            this.loadDrawPoints(drawPoints);
        }
        public void Clear()
        {
            this.clearFunc ();
        }

        public bool IsBlank {
            get { return this.isBlankFunc(); }
        }


        public void SetInternals(Func<ImageFormatType, Stream> getImage, Func<IEnumerable<DrawPoint>> getPoints, Action<IEnumerable<DrawPoint>> loadPoints,  Func<bool> isBlank,Action clear) {
            this.getImageFunc = getImage;
            this.getDrawPointsFunc = getPoints;
            this.loadDrawPoints = loadPoints;
            this.isBlankFunc = isBlank;
            this.clearFunc = clear;
        }

        #region Properties

        public static readonly BindableProperty CaptionTextProperty = BindableProperty.Create<SignaturePadView, string>(x => x.CaptionText, (string)null);
        public static readonly BindableProperty CaptionTextColorProperty = BindableProperty.Create<SignaturePadView, Color>(x => x.CaptionTextColor, Color.Default);
        public static readonly BindableProperty ClearTextProperty = BindableProperty.Create<SignaturePadView, string>(x => x.ClearText, (string)null);
        public static readonly BindableProperty ClearTextColorProperty = BindableProperty.Create<SignaturePadView, Color>(x => x.ClearTextColor, Color.Default);
        public static readonly BindableProperty PromptTextProperty = BindableProperty.Create<SignaturePadView, string>(x => x.PromptText, (string)null);
        public static readonly BindableProperty PromptTextColorProperty = BindableProperty.Create<SignaturePadView, Color>(x => x.PromptTextColor, Color.Default);
        public static readonly BindableProperty SignatureLineColorProperty = BindableProperty.Create<SignaturePadView, Color>(x => x.SignatureLineColor, Color.Default);
        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create<SignaturePadView, Color>(x => x.StrokeColor, Color.Default);
        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create<SignaturePadView, float>(x => x.StrokeWidth, (float)0);


        public string CaptionText {
            get { return (string)this.GetValue(SignaturePadView.CaptionTextProperty); }
            set { this.SetValue(SignaturePadView.CaptionTextProperty, value); }
        }


        public Color CaptionTextColor {
            get { return (Color)this.GetValue(SignaturePadView.CaptionTextColorProperty); }
            set { this.SetValue(SignaturePadView.CaptionTextColorProperty, value); }
        }


        public string ClearText {
            get { return (string)this.GetValue(SignaturePadView.ClearTextProperty); }
            set { this.SetValue(SignaturePadView.ClearTextProperty, value); }
        }


        public Color ClearTextColor {
            get { return (Color)this.GetValue(SignaturePadView.ClearTextColorProperty); }
            set { this.SetValue(SignaturePadView.ClearTextColorProperty, value); }
        }


        public string PromptText {
            get { return (string)this.GetValue(SignaturePadView.PromptTextProperty); }
            set { this.SetValue(SignaturePadView.PromptTextProperty, value); }
        }


        public Color PromptTextColor {
            get { return (Color)this.GetValue(SignaturePadView.PromptTextColorProperty); }
            set { this.SetValue(SignaturePadView.PromptTextColorProperty, value); }
        }


        public Color SignatureLineColor {
            get { return (Color)this.GetValue(SignaturePadView.SignatureLineColorProperty); }
            set { this.SetValue(SignaturePadView.SignatureLineColorProperty, value); }
        }


        public float StrokeWidth {
            get { return (float)this.GetValue(SignaturePadView.StrokeWidthProperty); }
            set { this.SetValue(SignaturePadView.StrokeWidthProperty, value); }
        }


        public Color StrokeColor {
            get { return (Color)this.GetValue(SignaturePadView.StrokeColorProperty); }
            set { this.SetValue(SignaturePadView.StrokeColorProperty, value); }
        }

        #endregion
    }
}
