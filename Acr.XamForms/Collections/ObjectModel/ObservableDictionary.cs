using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Collections.ObjectModel {

    ///<summary>
    /// This is a Dictionary that supports INotifyCollectionChanged semantics. 
    ///</summary>
    /// <remarks>
    /// WARNING: this dictionary is NOT thread-safe!  You must still
    /// provide synchronization to ensure no writes are done while the dictionary is being
    /// enumerated!  This should not be a problem for most bindings as they rely on the 
    /// CollectionChanged information.
    /// </remarks>
    ///<typeparam name="TKey">Key</typeparam>
    ///<typeparam name="TValue">Value type</typeparam>
    public class ObservableDictionary<TKey, TValue> : IObservableDictionary<TKey, TValue> {
        // Internal dictionary that holds values
        private readonly IDictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// Event raised for property change notifications
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event raised for collection change notification
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        public ObservableDictionary()
            : this(new Dictionary<TKey, TValue>()) {
        }

        /// <summary>
        /// Constructor that allows different storage initialization
        /// </summary>
        public ObservableDictionary(IDictionary<TKey, TValue> store) {
            if (store == null)
                throw new ArgumentNullException("store");

            _dictionary = store;
        }

        /// <summary>
        /// Constructor that takes an equality comparer
        /// </summary>
        /// <param name="comparer">Comparison class</param>
        public ObservableDictionary(IEqualityComparer<TKey> comparer)
            : this(new Dictionary<TKey, TValue>(comparer)) {
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.
        /// </param><param name="value">The object to use as the value of the element to add.
        /// </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        /// </exception><exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </exception><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.
        /// </exception>
        public void Add(TKey key, TValue value) {
            var item = new KeyValuePair<TKey, TValue>(key, value);
            _dictionary.Add(item);
            OnNotifyAdd(item);
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) {
            _dictionary.Add(item);
            OnNotifyAdd(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. 
        /// </exception>
        public void Clear() {
            _dictionary.Clear();
            OnNotifyReset();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the key; otherwise, false.
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        /// </exception>
        public bool ContainsKey(TKey key) {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            _dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key"/> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        /// <param name="key">The key of the element to remove.
        /// </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        /// </exception><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.
        /// </exception>
        public bool Remove(TKey key) {
            TValue local = _dictionary[key];
            bool flag = _dictionary.Remove(key);
            OnNotifyRemove(new KeyValuePair<TKey, TValue>(key, local));

            return flag;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) {
            bool flag = _dictionary.Remove(item);
            if (flag)
                OnNotifyRemove(item);
            return flag;
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        ///                 </param>
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) {
            return _dictionary.Contains(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key whose value to get.
        /// </param><param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.
        /// </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        /// </exception>
        public bool TryGetValue(TKey key, out TValue value) {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count {
            get { return _dictionary.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        /// The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set.
        /// </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.
        /// </exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key"/> is not found.
        /// </exception><exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.
        /// </exception>
        public TValue this[TKey key] {
            get { return _dictionary[key]; }
            set {
                if (_dictionary.ContainsKey(key)) {
                    var originalValue = _dictionary[key];
                    _dictionary[key] = value;
                    OnNotifyReplace(new KeyValuePair<TKey, TValue>(key, value), new KeyValuePair<TKey, TValue>(key, originalValue));
                }
                else {
                    _dictionary[key] = value;
                    OnNotifyAdd(new KeyValuePair<TKey, TValue>(key, value));
                }
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public ICollection<TKey> Keys {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public ICollection<TValue> Values {
            get { return _dictionary.Values; }
        }

        /// <summary>
        /// This is used to notify insertions into the dictionary.
        /// </summary>
        /// <param name="item">Item</param>
        private void OnNotifyAdd(KeyValuePair<TKey, TValue> item) {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            OnPropertyChanged(item.Key.ToString());
        }

        /// <summary>
        /// This is used to notify removals from the dictionary
        /// </summary>
        /// <param name="item">Item</param>
        private void OnNotifyRemove(KeyValuePair<TKey, TValue> item) {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            OnPropertyChanged(item.Key.ToString());
        }

        /// <summary>
        /// This is used to notify replacements in the dictionary
        /// </summary>
        /// <param name="newItem">New item</param>
        /// <param name="oldItem">Old item</param>
        private void OnNotifyReplace(KeyValuePair<TKey, TValue> newItem, KeyValuePair<TKey, TValue> oldItem) {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem));
            OnPropertyChanged(oldItem.Key.ToString());
        }

        /// <summary>
        /// This is used to notify that the dictionary was completely reset.
        /// </summary>
        private void OnNotifyReset() {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(string.Empty);
        }

        /// <summary>
        /// Raise the PropertyChanged notification for the dictionary.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(string.IsNullOrEmpty(propertyName) ? "" : "Item[" + propertyName + "]"));
        }

        /// <summary>
        /// Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged"/> event with the provided arguments.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (this.CollectionChanged != null)
                this.CollectionChanged(this, e);
            //var eh = CollectionChanged;
            //if (eh != null) {
            //    foreach (NotifyCollectionChangedEventHandler nh in eh.GetInvocationList())
            //        nh.Invoke(this, e);
            //}
        }
    }
}