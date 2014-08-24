using System;
using Xamarin.Forms;
using DataTemplate = System.Windows.DataTemplate;

[assembly: ExportRenderer(typeof(Acr.XamForms.Controls.EntryCell), typeof(Acr.XamForms.Controls.WindowsPhone.EntryCellRenderer))]


namespace Acr.XamForms.Controls.WindowsPhone {
    
    public class EntryCellRenderer : Xamarin.Forms.Platform.WinPhone.EntryCellRenderer {

        public override DataTemplate GetTemplate(Cell cell) {
            return base.GetTemplate(cell);
        }
    }
}