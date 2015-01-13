using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class UserDialogViewModel : ViewModel {
        private readonly IUserDialogService dialogService;


        public UserDialogViewModel(IUserDialogService dialogService) {
            this.dialogService = dialogService;
        }

        #region Bindings

        private string result;
        public string Result {
            get { return this.result; }
            private set { this.SetProperty(ref this.result, value); }
        }


        public ICommand Alert {
            get {
                return new Command(async () => {
                    await dialogService.AlertAsync("Test alert", "Alert Title", "CHANGE ME!");
                    this.Result = "Returned from alert!";
                });
            }
        }


        public ICommand ActionSheet {
            get {
                return new Command(() => {
                    var cfg = new ActionSheetConfig().SetTitle("Test Title");
                    for (var i = 0; i < 10; i++) {
                        var display = (i + 1);
                        cfg.Add("Option " + display, () => String.Format("Option {0} Selected", display));
                    }
                });
            }
        }


        public ICommand Confirm {
            get {
                return new Command(async () => {
                    var r = await dialogService.ConfirmAsync("Pick a choice", "Pick Title", "Yes", "No");
                    var text = (r ? "Yes" : "No");
                    this.Result = "Confirmation Choice: " + text;
                });
            }
        }


        public ICommand Login {
            get {
                return new Command(async () => {
                    var r = await dialogService.LoginAsync();
                    this.Result = String.Format(
                        "Login {0} - User Name: {1} - Password: {2}",
                        r.Ok ? "Success" : "Cancelled",
                        r.LoginText,
                        r.Password
                    );
                });
            }
        }

        public ICommand Prompt {
            get { return this.PromptCommand(false); }
        }


        public ICommand PromptSecure {
            get { return this.PromptCommand(true); }
        }


        public ICommand Progress {
            get {
                return new Command(async () => {
                    var cancelled = false;

                    using (var dlg = dialogService.Progress("Test Progress")) {
                        dlg.SetCancel(() => cancelled = true);
                        while (!cancelled && dlg.PercentComplete < 100) {
                            await Task.Delay(TimeSpan.FromMilliseconds(500));
                            dlg.PercentComplete += 2;
                        }
                    }
                    this.Result = (cancelled ? "Progress Cancelled" : "Progress Complete");                    
                });
            }
        }


        public ICommand ProgressNoCancel {
            get {
                return new Command(async () => {
                    using (var dlg = dialogService.Progress("Progress (No Cancel)")) {
                        while (dlg.PercentComplete < 100) {
                            await Task.Delay(TimeSpan.FromSeconds(1));
                            dlg.PercentComplete += 20;
                        }
                    }                    
                });
            }
        }


        public ICommand LoadingNoCancel {
            get {
                return new Command(async () => {
                    using (dialogService.Loading("Loading (No Cancel)")) 
                        await Task.Delay(TimeSpan.FromSeconds(3));
                    
                    this.Result = "Loading Complete";
                });
            }
        }


        public ICommand Loading {
            get {
                return new Command(async () => {
                    var cancelSrc = new CancellationTokenSource();

                    using (var dlg = dialogService.Loading("Loading")) {
                        dlg.SetCancel(cancelSrc.Cancel);

                        try { 
                            await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                        }
                        catch { }
                    }
                    this.Result = (cancelSrc.IsCancellationRequested ? "Loading Cancelled" : "Loading Complete");                    
                });
            }
        }


        public ICommand Toast {
            get {
                return new Command(() => {
                    this.Result = "Toast Shown";
                    dialogService.Toast("Test Toast", onClick: () => {
                        this.Result = "Toast Pressed";
                    });                    
                });
            }
        }


        private string customText;
        public string CustomText {
            get { return this.customText; }
            set { this.SetProperty(ref this.customText, value); }
        }


        public ICommand SingletonShowHide {
            get {
                return new Command(async () => {
                    this.dialogService.ShowLoading(this.CustomText);
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    this.dialogService.HideLoading();
                });
            }
        }


        #endregion

        #region Internals

        private ICommand PromptCommand(bool secure) {
            return new Command(async () => {
                var type = (secure ? "secure text" : "text");
                var r = await dialogService.PromptAsync(String.Format("Enter a {0} value", type.ToUpper()), secure: secure);
                this.Result = (r.Ok
                    ? "OK " + r.Text
                    : secure + " Prompt Cancelled"
                );
            });
        }

        #endregion
    }
}
