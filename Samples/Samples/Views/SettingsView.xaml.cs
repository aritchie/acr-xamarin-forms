using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Samples.ViewModels;


namespace Samples.Views {
    public partial class SettingsView : ContentPage {
        public SettingsView() {
            InitializeComponent();
			this.ListView.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null)
					return;

				((SettingsViewModel)this.BindingContext).Select.Execute(e.SelectedItem);
			};
        }
    }
}
