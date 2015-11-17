using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms.SignaturePad;
using Samples.Models;
using Xamarin.Forms;
using Acr;
using PCLStorage;
using Samples.Views;
using System.Diagnostics;

namespace Samples.ViewModels {

	public class SignatureListViewModel : ViewModel
    { 
		private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.{1}";
        private readonly ISignatureService signatureService;
        private IFolder folder = FileSystem.Current.LocalStorage;
        private Page MainPage {
            get
            {
                return App.Current.MainPage;
            }
        }

        public SignatureListViewModel() {
            signatureService = DependencyService.Get<ISignatureService>();
            this.Create = new Xamarin.Forms.Command(async () => await this.OnCreate());
			this.List = new ObservableCollection<Signature>();
            MessagingCenter.Subscribe<SignatureListView>(this, "Appearing", ((SignatureListView) =>
            {
                OnAppearing();
            }));
		}

        public void OnAppearing()
        {
            this.List.Clear();
            Task.Run(async () =>
            {
                var fileList = await FileSystem.Current.LocalStorage.GetFilesAsync();

                foreach (IFile file in fileList)
                {
                    using (Stream stream = await file.OpenAsync(FileAccess.Read))
                    {
                        var signature = new Signature
                        {
                            FileName = file.Name,
                            FilePath = file.Path,
                            FileSize = stream.Length
                        };

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            if (this.List.FirstOrDefault(x => x.FileName.Equals(signature.FileName)) == null)
                            {
                                this.List.Add(signature);
                            }
                        });
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    this.NoData = !this.List.Any();
                });
            });
        }


        public ObservableCollection<Signature> List { get; set; }

		private bool noData;
		public bool NoData {
			get { return this.noData; }
			set { this.SetProperty(ref this.noData, value); }
		}


		public ICommand Create { get; private set; }


		private async Task OnCreate() {
			var result = await this.signatureService.Request();

			if (result.Cancelled)
            {
                await App.Current.MainPage.DisplayAlert(null, "Canceled Signature", "Close");
			}
            else
            {
                //Grab the configured image type
                var imageType = signatureService.GetConfiguration().ImageType;

                var fileName =  String.Format(FILE_FORMAT, DateTime.Now, imageType.ToString().ToLower());
                long fileSize;

                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                using (var image = result.GetStream())
                {
                    using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.CopyTo(ms);

                            byte[] buffer = ms.ToArray();
                            stream.Write(buffer, 0, buffer.Length);
                            fileSize = buffer.Length;
                            await stream.FlushAsync();
                        }   
                    }
                }

                this.List.Add(new Signature
                {
                    FilePath = file.Path,
                    FileName = file.Name,
                    FileSize = fileSize
                });

                await App.Current.MainPage.DisplayAlert(null, String.Format("Draw Points: {0}", result.Points.Count()), "Close");

                this.NoData = !this.List.Any();
			}
		}

		private Xamarin.Forms.Command<Signature> selectCmd;
        /// <summary>
        /// Handles the Selection of a Saved Signature. Allows user to view/delete
        /// </summary>
		public Xamarin.Forms.Command<Signature> Select
        {
            get
            {
                this.selectCmd = this.selectCmd ?? new Xamarin.Forms.Command<Signature>(async (signature) =>
                {
                    var action = await App.Current.MainPage.DisplayActionSheet(null, "Cancel", null, "View", "Delete");

                    if (!string.IsNullOrEmpty(action) && !action.Equals("Cancel"))
                    {
                        if (action.Equals("View"))
                        {
                            await BuildViewPage(signature);
                        }
                        else if (action.Equals("Delete"))
                        {
                            var deleteAction = await MainPage.DisplayActionSheet(String.Format("Are you sure you want to delete {0}", signature.FileName), null, null, "Yes", "No");
                            if ("No".Equals(deleteAction))
                                return;
                            try
                            {
                                var file = await folder.GetFileAsync(signature.FileName);
                                await file.DeleteAsync();
                            }
                            catch (FileNotFoundException e)
                            {
                                #if DEBUG
                                Debug.WriteLine(e.StackTrace);
                                #endif
                            }
                            finally
                            {
                                this.List.Remove(signature);
                                this.NoData = !this.List.Any();
                            }
                        }
                    }
                });
                return this.selectCmd;
            }
        }

        /// <summary>
        /// Builds the Content Page to Show the save signature and displays it in a modal
        /// </summary>
        /// <param name="signature"></param>
        /// <returns></returns>
        private async Task BuildViewPage(Signature signature)
        {
            try
            {
                #region Create Signature Image control
                var img = new Image
                {
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
                img.Source = ImageSource.FromFile(new Uri(signature.FilePath).AbsolutePath);
                #endregion

                #region Close Button
                var button = new Button();
                button.Text = "Close";
                button.Clicked += (snd, evtArgs) =>
                {
                    App.Navigation.PopModalAsync();
                };
                #endregion

                #region Content Page
                ContentPage imageDisplayModal = new ContentPage
                {
                    Content = new StackLayout
                    {
                        Children =
                        {
                            img,
                            button
                        }
                    }
                };
                #endregion
                await MainPage.Navigation.PushModalAsync(imageDisplayModal);
            }
            catch
            {
                await MainPage.DisplayAlert(null, "Cannot open file", "Close");
            }
        }
    }
}

