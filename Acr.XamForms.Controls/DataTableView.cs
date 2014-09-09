//using System;
//using System.Collections;
//using Xamarin.Forms;


//namespace Acr.XamForms.Controls {
    
//    public class DataTableView : TableView {

//        public static readonly BindableProperty RowItemTemplateProperty = BindableProperty.Create("RowItemTemplate", typeof(DataTemplate), typeof(DataTableView), null);
//        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<DataTableView, IEnumerable>(o => o.ItemsSource, null, propertyChanged: DataTableView.OnItemsSourceChanged);


//        public IEnumerable ItemsSource {
//            get { return (IEnumerable)this.GetValue(DataTableView.ItemsSourceProperty); }
//            set { this.SetValue(DataTableView.ItemsSourceProperty, value); }
//        }


//        public DataTemplate ItemTemplate {
//            get { return (DataTemplate)this.GetValue(DataTableView.ItemTemplateProperty); }
//            set { this.SetValue(DataTableView.ItemTemplateProperty, value); }
//        }

        
//        private static void OnItemsSourceChanged(BindableObject obj, IEnumerable oldValue, IEnumerable newValue) {
//            var view = (DataTableView)obj;
//            view.Root.Clear();
//            var section = new TableSection();

//            foreach (var item in view.ItemsSource) {
//                view.ItemTemplate.CreateContent();

//                //section.Add();
//            }
//        }
//    }
//}


