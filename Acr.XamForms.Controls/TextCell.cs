using System;
using Xamarin.Forms;


namespace Acr.XamForms.Controls {
    
    public class TextCell : Xamarin.Forms.TextCell {
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create<EntryCell, bool>(x => x.IsVisible, true);


        public bool IsVisible {
            get { return (bool)this.GetValue(IsVisibleProperty); }
            set { this.SetValue(IsVisibleProperty, value); }
        }
    }
}
