using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms.SignaturePad;
using Samples.Models;
using Xamarin.Forms;
using Acr.UserDialogs;
using Acr;
using PCLStorage;

namespace Samples.ViewModels {

	public class SignatureListViewModel : ViewModel
    { 
		private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.jpg";
		private readonly ISignatureService signatureService;
        private IFolder folder = FileSystem.Current.LocalStorage;

        public SignatureListViewModel() {
            signatureService = DependencyService.Get<ISignatureService>();
            this.Create = new Xamarin.Forms.Command(async () => await this.OnCreate());
			this.List = new ObservableCollection<Signature>();
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
                            this.List.Add(signature);
                        });
                    }
                }
            });
            this.NoData = !this.List.Any();
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
				var fileName = String.Format(FILE_FORMAT, DateTime.Now);
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
		public Xamarin.Forms.Command<Signature> Select {
			get {
				this.selectCmd = this.selectCmd ?? new Xamarin.Forms.Command<Signature>(s =>
                    UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
						.Add("View", () => {
                            try {
							    Device.OpenUri(new Uri("file://" + s.FilePath));
                            }
                            catch {
                                UserDialogs.Instance.Alert("Cannot open file");
                            }
						})
						.Add("Delete", async () => {
							var r = await UserDialogs.Instance.ConfirmAsync(String.Format("Are you sure you want to delete {0}", s.FileName));
							if (!r)
								return;

                            var file = await folder.GetFileAsync(s.FileName);
                            await file.DeleteAsync();
			                this.List.Remove(s);
							this.NoData = !this.List.Any();
						})
						.Add("Cancel")
					)
				);
				return this.selectCmd;
			}
		}
	}
}

