using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;


namespace System.Collections.ObjectModel {

    public interface IObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged {}
}
