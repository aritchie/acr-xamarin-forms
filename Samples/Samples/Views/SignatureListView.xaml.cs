using System;
using Xamarin.Forms;
using Samples.ViewModels;
using Ninject;

namespace Samples.Views {

    public partial class SignatureListView : ContentPage {

        public SignatureListView() {
            this.BindingContext = App.Kernel.Get<SignatureListViewModel>();
            InitializeComponent();
			this.ListView.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null)
					return;

				((SignatureListViewModel)this.BindingContext).Select.Execute(e.SelectedItem);
			};
        }
    }
}
