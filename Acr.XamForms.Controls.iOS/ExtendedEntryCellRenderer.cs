using System;
using Acr.XamForms.Controls;
using Acr.XamForms.Controls.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntryCell), typeof(ExtendedEntryCellRenderer))]


namespace Acr.XamForms.Controls.iOS {
    
    public class ExtendedEntryCellRenderer : EntryCellRenderer {

        public override UITableViewCell GetCell(Cell item, UITableView tv) {
            var entryCell = (ExtendedEntryCell)item;
            var tableCell = base.GetCell(item, tv);

            var txt = tableCell.ContentView.Subviews[0] as UITextField;
            txt.SecureTextEntry = entryCell.IsPassword;
            
            tableCell.Hidden = !entryCell.IsVisible;
            
            if (!entryCell.IsSeparatorVisible)
                tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            else if (entryCell.SeperatorColor != Color.Default) 
                tv.SeparatorColor = entryCell.SeperatorColor.ToUIColor();

            return tableCell;
        }
    }
}