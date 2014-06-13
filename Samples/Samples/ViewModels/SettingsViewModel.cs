using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.XamForms.Settings;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Samples.ViewModels {

    public class SettingsViewModel : ViewModel {
        private readonly ISettings settings;
        private readonly IUserDialogService dialogs;


        public SettingsViewModel(ISettings settings, IUserDialogService dialogs) {
            this.settings = settings;
            this.dialogs = dialogs;
        }


        public IDictionary<string, string> Settings {
            get { return this.settings.All; }
        }


        public Command<KeyValuePair<string, string>> Select {
            get {
                return new Command<KeyValuePair<string, string>>(setting => this.dialogs.ActionSheet(x => x
                    .SetTitle("Item Actions")
                    .Add("Remove", async () => {
                        var r = await this.dialogs.ConfirmAsync("Are you sure you wish to remove " + setting.Key);
                        if (r)
                            this.settings.Remove(setting.Key);
                    })
                    .Add("Edit", async () => {
                        var r = await this.dialogs.PromptAsync("Update setting " + setting.Key);
                        if (r.Ok)
                            this.settings.Set(setting.Key, r.Text);
                    })
                    .Add("Cancel")
                ));
            }
        } 


        public ICommand Actions {
            get {

                return new Command(() => this.dialogs.ActionSheet(x => x
                    .SetTitle("Actions")
                    .Add("Add Setting", () => this.OnAdd())
                    .Add("Clear All", () => this.OnClear())
                ));
            }
        }


        private async Task OnAdd() {
            var key = await this.dialogs.PromptAsync("Enter key");
            if (!key.Ok || String.IsNullOrWhiteSpace(key.Text)) 
                return;

            var value = await this.dialogs.PromptAsync("Enter value");
            if (!value.Ok)
                return;

            this.settings.Set(key.Text, value.Text);
        }


        private async Task OnClear() {
            var r = await this.dialogs.ConfirmAsync("Are you sure you want to clear settings?");
            if (r)
                this.settings.Clear();
        }
    }
}
