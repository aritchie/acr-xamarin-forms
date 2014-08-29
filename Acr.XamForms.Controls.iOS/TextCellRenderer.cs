using System;
using System.ComponentModel;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Acr.XamForms.Controls.TextCell), typeof(Acr.XamForms.Controls.iOS.TextCellRenderer))]


namespace Acr.XamForms.Controls.iOS {
    
    public class TextCellRenderer : Xamarin.Forms.Platform.iOS.TextCellRenderer {
        private TextCell cell;
        private UITableView tableView;
        private UITableViewCell tableViewCell;
        private NSIndexPath index;


        public override UITableViewCell GetCell(Cell item, UITableView tv) {
            this.cell = (TextCell)item;
            this.tableView = tv;
            this.tableViewCell = base.GetCell(item, tv);

            this.cell.PropertyChanged += this.OnCellPropertyChanged;
            this.index = this.tableView.IndexPathForCell(this.tableViewCell);
            this.SetVisible();

            return this.tableViewCell;
        }


        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args) {
            switch (args.PropertyName) {
                case "IsVisible":
                    this.tableViewCell.Hidden = !this.cell.IsVisible;
                    break;
            }
        }


        private void SetVisible() {
            if (this.cell.IsVisible) 
                this.tableView.InsertRows(new [] { this.index }, UITableViewRowAnimation.Fade);
            else 
                this.tableView.DeleteRows(new [] { this.index }, UITableViewRowAnimation.Fade);

            this.tableViewCell.Hidden = !this.cell.IsVisible;
            
        }
    }
}