using System;
using System.ComponentModel;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using EntryCell = Acr.XamForms.Controls.EntryCell;
using EntryCellRenderer = Acr.XamForms.Controls.iOS.EntryCellRenderer;

[assembly: ExportRenderer(typeof(EntryCell), typeof(EntryCellRenderer))]


namespace Acr.XamForms.Controls.iOS {
    
    public class EntryCellRenderer : Xamarin.Forms.Platform.iOS.EntryCellRenderer {
        private EntryCell cell;
        private UITableView tableView;
        private UITableViewCell tableViewCell;
        private UITextField textField;


        public override UITableViewCell GetCell(Cell item, UITableView tv) {
            this.cell = (EntryCell)item;
            this.tableViewCell = base.GetCell(item, tv);
            this.tableView = tv;
            this.textField = (UITextField)this.tableViewCell.ContentView.Subviews[0];

            this.cell.PropertyChanged += this.OnCellPropertyChanged;
            this.textField.SecureTextEntry = this.cell.IsPassword;
            this.tableViewCell.Hidden = !this.cell.IsVisible;

            return this.tableViewCell;
        }


        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args) {
            switch (args.PropertyName) {
                case "IsPassword":
                    this.textField.SecureTextEntry = this.cell.IsPassword;
                    break;

                case "IsVisible":
                    this.tableViewCell.Hidden = !this.cell.IsVisible;
                    this.tableView.ReloadData();
                    break;
            }
        }
    }
}