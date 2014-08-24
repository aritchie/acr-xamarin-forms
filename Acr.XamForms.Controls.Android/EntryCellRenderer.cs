using System;
using System.ComponentModel;
using Android.Content;
using Android.Text;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(Acr.XamForms.Controls.EntryCell), typeof(Acr.XamForms.Controls.Android.EntryCellRenderer))]


namespace Acr.XamForms.Controls.Android {
    
    public class EntryCellRenderer : Xamarin.Forms.Platform.Android.EntryCellRenderer {
        private EntryCellView view;


        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context) {
            var entry = (EntryCell)item;
            this.view = (EntryCellView)base.GetCellCore(item, convertView, parent, context);
            this.view.Visibility = entry.IsVisible
                ? ViewStates.Visible
                : ViewStates.Gone;

            if (entry.IsPassword)
                this.view.EditText.InputType = InputTypes.TextVariationPassword;

            return this.view;
        }


        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnCellPropertyChanged(sender, e);
        }
    }
}