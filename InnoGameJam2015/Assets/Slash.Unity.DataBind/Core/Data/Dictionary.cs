namespace Slash.Unity.DataBind.Core.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DataDictionary<TKey, TValue> : DataDictionary, IDictionary<TKey, TValue>
    {
        #region Fields

        private readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        #endregion

        #region Properties

        public override int Count
        {
            get
            {
                return this.dictionary.Count;
            }
        }

        public override bool IsFixedSize
        {
            get
            {
                return ((IDictionary)this.dictionary).IsFixedSize;
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                return ((ICollection)this.dictionary).IsSynchronized;
            }
        }

        public override object this[object key]
        {
            get
            {
                return this.dictionary[(TKey)key];
            }
            set
            {
                this.dictionary[(TKey)key] = (TValue)value;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.dictionary[key];
            }
            set
            {
                this.dictionary[key] = value;
            }
        }

        public override ICollection Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override object SyncRoot
        {
            get
            {
                return ((ICollection)this.dictionary).SyncRoot;
            }
        }

        public override ICollection Values
        {
            get
            {
                return this.dictionary.Values;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return this.dictionary.Keys;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return this.dictionary.Values;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void Add(object key, object value)
        {
            this.Add((TKey)key, (TValue)value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            this.dictionary.Add(key, value);
            this.OnCollectionChanged();
        }

        public override void Clear()
        {
            if (this.dictionary.Count == 0)
            {
                return;
            }

            this.dictionary.Clear();
            this.OnCollectionChanged();
        }

        public override bool Contains(object key)
        {
            return this.dictionary.ContainsKey((TKey)key);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public override void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override IDictionaryEnumerator GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        public override void Remove(object key)
        {
            this.Remove((TKey)key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            if (this.dictionary.Remove(key))
            {
                this.OnCollectionChanged();
                return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        #endregion

        #region Methods

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        #endregion
    }

    public abstract class DataDictionary : IDictionary
    {
        #region Delegates

        public delegate void CollectionChangedDelegate();

        #endregion

        #region Events

        public event CollectionChangedDelegate CollectionChanged;

        #endregion

        #region Properties

        public abstract int Count { get; }

        public abstract bool IsFixedSize { get; }

        public abstract bool IsSynchronized { get; }

        public abstract object this[object key] { get; set; }

        public abstract ICollection Keys { get; }

        public abstract object SyncRoot { get; }

        public abstract ICollection Values { get; }

        bool IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Public Methods and Operators

        public abstract void Add(object key, object value);

        public abstract void Clear();

        public abstract bool Contains(object key);

        public abstract void CopyTo(Array array, int index);

        public abstract IDictionaryEnumerator GetEnumerator();

        public abstract void Remove(object key);

        #endregion

        #region Methods

        protected virtual void OnCollectionChanged()
        {
            var handler = this.CollectionChanged;
            if (handler != null)
            {
                handler();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}