using System;
using Acr.XamForms.Controls;
using Acr.XamForms.Controls.WindowsPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using DataTemplate = System.Windows.DataTemplate;

[assembly: ExportRenderer(typeof(ExtendedEntryCell), typeof(ExtendedEntryCellRenderer))]


namespace Acr.XamForms.Controls.WindowsPhone {
    
    public class ExtendedEntryCellRenderer : EntryCellRenderer {

        public override DataTemplate GetTemplate(Cell cell) {
            return base.GetTemplate(cell);
        }
    }
}