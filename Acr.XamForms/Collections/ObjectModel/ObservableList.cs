using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace System.Collections.ObjectModel {
    
    public class ObservableList<T> : ObservableCollection<T> {

        public ObservableList() { }
        public ObservableList(IEnumerable<T> en) : base(en) { } 


        public void AddRange(IEnumerable<T> en) {
            foreach (var item in en)
                this.Items.Add(item);

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, en));
        }
    }
}
