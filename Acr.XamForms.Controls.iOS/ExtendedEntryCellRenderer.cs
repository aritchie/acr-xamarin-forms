using System;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


namespace Acr.XamForms.Controls.iOS {
    
    public class ExtendedEntryCellRenderer : EntryCellRenderer {

        public override UITableViewCell GetCell(Cell item, UITableView tv) {
            var entryCell = (ExtendedEntryCell)item;
            var tableCell = base.GetCell(item, tv);
            var txt = tableCell.ContentView.Subviews[1] as UITextField;

            tableCell.Hidden = !entryCell.IsVisible;
            txt.SecureTextEntry = entryCell.IsPassword;
            return tableCell;
        }
    }
}