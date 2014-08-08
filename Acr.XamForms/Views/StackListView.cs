//using System;
//using System.Collections;
//using Xamarin.Forms;


//namespace Acr.XamForms.Views {
    
//    public class StackListView : StackLayout {

//        #region Properties
        
//        public static BindableProperty SelectedItemProperty = BindableProperty.Create<StackListView, object>(x => x.SelectedItem, null, BindingMode.TwoWay, propertyChanged: StackListView.OnSelectedItemChanged);
//        public static BindableProperty ItemsSourceProperty = BindableProperty.Create<StackListView, IEnumerable>(o => o.ItemsSource, null, propertyChanged: StackListView.OnItemsSourceChanged);


//        public IEnumerable ItemsSource {
//            get { return (IEnumerable)this.GetValue(Picker.ItemsSourceProperty); }
//            set { this.SetValue(Picker.ItemsSourceProperty, value); }
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

//            if (newValue != null) 
//                foreach (var item in newValue)
//                    layout.Children.Add(null); // TODO: data template
//        }


//        private static void OnSelectedItemChanged(BindableObject obj, object oldValue, object newValue) {
//            var layout = (StackListView)obj;
//            if (newValue != null)
//                layout.SelectedIndex = layout.Items.IndexOf(newValue.ToString());
//        }

//        #endregion
//    }
//}
