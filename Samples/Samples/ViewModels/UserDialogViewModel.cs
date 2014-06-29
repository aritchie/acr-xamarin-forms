using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class UserDialogViewModel : ViewModel {

        public ICommand Alert { get; private set; }
        public ICommand ActionSheet { get; private set; }
        public ICommand Confirm { get; private set; }
        public ICommand Progress { get; private set; }
        public ICommand ProgressNoCancel { get; private set; }
        public ICommand Loading { get; private set; }
        public ICommand LoadingNoCancel { get; private set; }
        public ICommand Prompt { get; private set; }
        public ICommand Toast { get; private set; }


        private string result;
        public string Result {
            get { return this.result; }
            private set { this.SetProperty(ref this.result, value); }
        }


        public UserDialogViewModel(IUserDialogService dialogService) {
            this.ActionSheet = new Command(() => 
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

            this.Alert = new Command(async () => {
                await dialogService.AlertAsync("Test alert", "Alert Title", "CHANGE ME!");
                this.Result = "Returned from alert!";
            });

            this.Confirm = new Command(async () => {
                var r = await dialogService.ConfirmAsync("Pick a choice", "Pick Title", "Yes", "No");
                var text = (r ? "Yes" : "No");
                this.Result = "Confirmation Choice: " + text;
            });

            this.Prompt = new Command(async () => {
                var r = await dialogService.PromptAsync("Enter a value");
                this.Result = (r.Ok
                    ? "OK " + r.Text
                    : "Prompt Cancelled"
                );
            });

            this.Toast = new Command(() => {
                this.Result = "Toast Shown";
                dialogService.Toast("Test Toast", onClick: () => {
                    this.Result = "Toast Pressed";
                });
            });

            this.Progress = new Command(async () => {
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

            this.ProgressNoCancel = new Command(async () => {
                using (var dlg = dialogService.Progress("Progress (No Cancel)")) {
                    while (dlg.PercentComplete < 100) {
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        dlg.PercentComplete += 20;
                    }
                }
            });

            this.Loading = new Command(async () => {
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

            this.LoadingNoCancel = new Command(async () => {
                using (dialogService.Loading()) {
                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
                this.Result = "Loading Complete";
            });
        }
    }
}
