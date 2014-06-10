using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms;
using Acr.XamForms.Mobile;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.ViewModels;
using PCLStorage;
using Samples.Models;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class SignatureListViewModel : ViewModel {

        private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.jpg";
        private readonly ISignatureService signatureService;
        private readonly IFileViewer fileViewer;


        public SignatureListViewModel(ISignatureService signatureService, IFileViewer fileViewer) {
            this.signatureService = signatureService;
            this.fileViewer = fileViewer;

            this.Configure = new Command(() => App.NavigateTo<SignaturePadConfigViewModel>());
            this.Create = new Command(this.OnCreate);
            this.Delete = new Command<Signature>(x => this.OnDelete(x));
            this.View = new Command<Signature>(this.OnView);
            this.List = new ObservableCollection<Signature>();
        }


        public override async Task Start() {
            var files = await FileSystem.Current.LocalStorage.GetFilesAsync();

            files
                .Select(x => new Signature {
                    FileName = Path.GetFileName(x.Name),
                    FilePath = x.Path,
                    FileSize = Int32.MaxValue // TODO: PCLStorage sucks IMO!  No filesize, everything async where it doesn't need to be!  BLAH!
                })
                .ToList()
                .ForEach(this.List.Add);
        }


        public ObservableCollection<Signature> List { get; private set; }

        private bool noData;
        public bool NoData {
            get { return this.noData; }
            set { this.SetProperty(ref this.noData, value); }
        }


        public ICommand Configure { get; private set; }
        public ICommand Create { get; private set; }
        public Command<Signature> View { get; private set; }
        public Command<Signature> Delete { get; private set; }


        private void OnCreate() {
            this.signatureService.RequestSignature(async result => {
                var fileName = String.Format(FILE_FORMAT, DateTime.Now);

                using (var ms = new MemoryStream()) {
                    result.Stream.CopyTo(ms);
                    var bytes = ms.ToArray();
                    var file = await FileSystem.Current.LocalStorage.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
                    using (var fs = await file.OpenAsync(FileAccess.ReadAndWrite))
                        fs.Write(bytes, 0, bytes.Length);
                }
                this.List.Add(new Signature {
                    FilePath = fileName,
                    FileName = fileName,
                    FileSize = Int32.MaxValue // TODO: PCLStorage sucks IMO!  No filesize, everything async where it doesn't need to be!  BLAH!
                });
                this.NoData = !this.List.Any();
            }); 
        }


        private void OnView(Signature signature) {
            this.fileViewer.Open(signature.FilePath);
        }


        private async Task OnDelete(Signature signature) {
            var file = await FileSystem.Current.LocalStorage.GetFileAsync(signature.FilePath);
            await file.DeleteAsync();
            this.List.Remove(signature);
            this.NoData = !this.List.Any();
        }
    }
}

