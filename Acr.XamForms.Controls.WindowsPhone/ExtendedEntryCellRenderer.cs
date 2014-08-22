using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using DataTemplate = System.Windows.DataTemplate;


namespace Acr.XamForms.Controls.WindowsPhone {
    
    public class ExtendedEntryCellRenderer : EntryCellRenderer {

        public override DataTemplate GetTemplate(Cell cell) {
            return base.GetTemplate(cell);
        }
    }
}