using System;
using System.Collections;
using Xamarin.Forms;


namespace Acr.XamForms.Controls {

    public class Picker : Xamarin.Forms.Picker {

        public Picker() {
            this.SelectedIndexChanged += this.OnSelectedIndexChanged;
        }


        #region Properties

        public static BindableProperty SelectedItemProperty = BindableProperty.Create<Picker, object>(x => x.SelectedItem, null, bindingPropertyChanged: Picker.OnSelectedItemChanged);
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create<Picker, IEnumerable>(o => o.ItemsSource, null, bindingPropertyChanged: Picker.OnItemsSourceChanged);


        public IEnumerable ItemsSource {
            get { return (IEnumerable)this.GetValue(Picker.ItemsSourceProperty); }
            set { this.SetValue(Picker.ItemsSourceProperty, value); }
        }



        public object SelectedItem {
            get { return this.GetValue(Picker.SelectedItemProperty); }
            set { this.SetValue(Picker.SelectedItemProperty, value); }
        }

        #endregion

        #region Internals

        private void OnSelectedIndexChanged(object sender, EventArgs args) {
            this.SelectedItem = (this.SelectedIndex < 0
                ? null
                : this.Items[this.SelectedIndex]
            );
        }


        private static void OnItemsSourceChanged(BindableObject obj, IEnumerable oldValue, IEnumerable newValue) {
            var picker = (Picker)obj;
            picker.Items.Clear();

            if (newValue != null) 
                foreach (var item in newValue)
                    picker.Items.Add(item.ToString());
            
        }


        private static void OnSelectedItemChanged(BindableObject obj, object oldValue, object newValue) {
            var picker = (Picker)obj;
            if (newValue != null)
                picker.SelectedIndex = picker.Items.IndexOf(newValue.ToString());
        }

        #endregion
    }
}
 No newline at end of file
