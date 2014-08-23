using System;
using Acr.XamForms.Controls;
using Acr.XamForms.Controls.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedTextCell), typeof(ExtendedTextCellRenderer))]


namespace Acr.XamForms.Controls.iOS {
    
    public class ExtendedTextCellRenderer : TextCellRenderer {
        public override UITableViewCell GetCell(Cell item, UITableView tv) {
            return base.GetCell(item, tv);
        }
    }
}