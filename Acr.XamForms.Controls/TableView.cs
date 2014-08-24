using System;
using Xamarin.Forms;


namespace Acr.XamForms.Controls {
    
    public class TableView : Xamarin.Forms.TableView {

        public static readonly BindableProperty IsSeparatorVisibleProperty = BindableProperty.Create<TableView, bool>(x => x.IsSeparatorVisible, true);
        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create<TableView, Color>(x => x.SeperatorColor, Color.Default);


        public bool IsSeparatorVisible {
            get { return (bool)this.GetValue(IsSeparatorVisibleProperty); }
            set { this.SetValue(IsSeparatorVisibleProperty, value); }
        }


        public Color SeperatorColor {
            get { return (Color)this.GetValue(SeparatorColorProperty); }
            set { this.SetValue(SeparatorColorProperty, value); }
        }
    }
}
