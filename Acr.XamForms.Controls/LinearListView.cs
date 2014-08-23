//using System;
//using System.Collections;
//using Xamarin.Forms;


//namespace Acr.XamForms.Views {
    
//    public class StackListView : StackLayout {

//        #region Properties

//        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(StackListView), null);
//        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<StackListView, IEnumerable>(o => o.ItemsSource, null, propertyChanged: StackListView.OnItemsSourceChanged);
//        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create<StackListView, object>(x => x.SelectedItem, null, BindingMode.TwoWay, propertyChanged: StackListView.OnSelectedItemChanged);
//        public static readonly BindableProperty RowHeightProperty = BindableProperty.Create("RowHeight", typeof(int), typeof(StackListView), 100);


//        public IEnumerable ItemsSource {
//            get { return (IEnumerable)this.GetValue(StackListView.ItemsSourceProperty); }
//            set { this.SetValue(StackListView.ItemsSourceProperty, value); }
//        }


//        public DataTemplate ItemTemplate {
//            get { return (DataTemplate)this.GetValue(StackListView.ItemTemplateProperty); }
//            set { this.SetValue(StackListView.ItemTemplateProperty, value); }
//        }


//        public int RowHeight {
//            get { return (int)this.GetValue(StackListView.RowHeightProperty); }
//            set { this.SetValue(StackListView.RowHeightProperty, value); }
//        }


//        public object SelectedItem {
//            get { return this.GetValue(Picker.SelectedItemProperty); }
//            set { this.SetValue(Picker.SelectedItemProperty, value); }
//        }

//        #endregion

//        #region Internals

//        private static void OnItemsSourceChanged(BindableObject obj, IEnumerable oldValue, IEnumerable newValue) {
//            var layout = (StackListView)obj;
//            layout.Children.Clear();

//            if (newValue != null) { 
//                foreach (var item in newValue) {
//                    var view = CreateView(layout, item);
//                    layout.Children.Add(view);
//                }
//            }
//        }


//        private static View CreateView(StackListView layout, object value) {
//            var el = layout.ItemTemplate.CreateContent();
//            return null;
//        }


//        private static void OnSelectedItemChanged(BindableObject obj, object oldValue, object newValue) {
//            var layout = (StackListView)obj;
//            //if (newValue != null)
//            //    layout.SelectedIndex = layout.Items.IndexOf(newValue.ToString());
//        }

//        #endregion
//    }
//}
