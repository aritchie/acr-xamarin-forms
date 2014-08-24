using System;
using Xamarin.Forms;


namespace Acr.XamForms.Controls {
    
    public class EntryCell : Xamarin.Forms.EntryCell {
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create<EntryCell, bool>(x => x.IsPassword, false);
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create<EntryCell, bool>(x => x.IsVisible, true);


        public bool IsPassword {
            get { return (bool)this.GetValue(IsPasswordProperty); }
            set { this.SetValue(IsPasswordProperty, value); }
        }


        public bool IsVisible {
            get { return (bool)this.GetValue(IsVisibleProperty); }
            set { this.SetValue(IsVisibleProperty, value); }
        }
    }
}
