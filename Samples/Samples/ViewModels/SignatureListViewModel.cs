using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.XamForms.SignaturePad;
using Acr.XamForms.ViewModels;
using Samples.Models;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class SignatureListViewModel : ViewModel {

        private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.jpg";
        private readonly ISignatureService signatureService;


        public SignatureListViewModel(ISignatureService signatureService) {
            this.signatureService = signatureService;
            //this.Configure = new Command(() => this.ShowViewModel<SignaturePadConfigViewModel>());
            this.Create = new Command(this.OnCreate);
            this.Delete = new Command<Signature>(this.OnDelete);
            this.View = new Command<Signature>(this.OnView);
            this.List = new ObservableCollection<Signature>();
        }


        //public override async Task Start() {
            //var files = this.store
            //    .GetFilesIn(".")
            //    .Select(x => new Signature {
            //        FileName = Path.GetFileName(x),
            //        FilePath = x
            //    })
            //    .ToList();

            //foreach (var file in files)
            //    this.List.Add(file);
        //}


        public ObservableCollection<Signature> List { get; private set; }
        public ICommand Configure { get; private set; }
        public ICommand Create { get; private set; }
        public Command<Signature> View { get; private set; }
        public Command<Signature> Delete { get; private set; }


        private void OnCreate() {
            this.signatureService.RequestSignature(result => {
                var fileName = String.Format(FILE_FORMAT, DateTime.Now);
                //var path = this.store.NativePath(fileName);

                //using (var ms = new MemoryStream()) {
                //    result.Stream.CopyTo(ms);
                //    var bytes = ms.ToArray();
                //    this.store.WriteFile(path, bytes);
                //}
                //this.List.Add(new Signature {
                //    FilePath = path,
                //    FileName = fileName
                //});
            });      
        }


        private void OnView(Signature signature) {
            //var path = this.store.NativePath(signature.FileName);
            // TODO: open
        }


        private void OnDelete(Signature signature) {
            //this.store.DeleteFile(signature.FilePath);
            this.List.Remove(signature);
        }
    }
}

