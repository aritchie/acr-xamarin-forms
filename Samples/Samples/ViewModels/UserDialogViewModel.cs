using System;
using System.Globalization;
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
                return new Command(() => 
                    dialogService.ActionSheet(new ActionSheetConfig()
                        .SetTitle("Test Title")
                        .Add("Option 1", () => this.Result = "Option 1 Selected")
                        .Add("Option 2", () => this.Result = "Option 2 Selected")
                        .Add("Option 3", () => this.Result = "Option 3 Selected")
                        .Add("Option 4", () => this.Result = "Option 4 Selected")
                        .Add("Option 5", () => this.Result = "Option 5 Selected")
                        .Add("Option 6", () => this.Result = "Option 6 Selected")
                    )
                );
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


        public ICommand Loading {
            get {
                return new Command(async () => {
                    using (dialogService.Loading()) 
                        await Task.Delay(TimeSpan.FromSeconds(3));
                    
                    this.Result = "Loading Complete";                    
                });
            }
        }


        public ICommand LoadingNoCancel {
            get {
                return new Command(async () => {
                    var cancelSrc = new CancellationTokenSource();

                    using (var dlg = dialogService.Loading("Test Progress")) {
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


        public ICommand DatePrompt {
            get {
                return this.DateTimeCommand(
                    DateTimeSelectionType.Date, 
                    CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern, 
                    "Date", 
                    DateTime.Now.AddDays(-3), 
                    DateTime.Now.AddDays(30)
                );
            }
        }


        public ICommand TimePrompt {
            get {
                return this.DateTimeCommand(
                    DateTimeSelectionType.Time, 
                    CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern, 
                    "Time", 
                    DateTime.Now.AddHours(-3), 
                    DateTime.Now.AddHours(3)
                );                    
            }
        }


        public ICommand DateTimePrompt {
            get {
                return this.DateTimeCommand(
                    DateTimeSelectionType.DateTime, 
                    CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, 
                    "Date/Time",
                    DateTime.Now.AddDays(-3), 
                    DateTime.Now.AddDays(30)
                );
            }
        }


        public ICommand DurationPrompt {
            get {
                return new Command(async () => {
                    var r = await dialogService.DurationPromptAsync(new DurationPromptConfig()
                        .SetTitle("Duration")
                        //.SetRange()
                    );
                    this.Result = r.Success
                        ? "Duration: " + r.SelectedTimeSpan.Value
                        : "Duration Prompt Cancelled";
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


        private ICommand DateTimeCommand(DateTimeSelectionType type, string resultFormat, string title, DateTime min, DateTime max) {
            return new Command(async () => {
                var r = await this.dialogService.DateTimePromptAsync(new DateTimePromptConfig()
                    .SetRange(min, max)
                    .SetTitle(title)
                    .SetSelectionType(type)
                );
                    
                this.Result = r.Success
                    ? String.Format("{0:" + resultFormat + "}", r.SelectedDateTime)
                    : title + " selection was cancelled";
            });
        }

        #endregion
    }
}
