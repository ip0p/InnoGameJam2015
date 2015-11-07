namespace Slash.Unity.DataBind.Foundation.Providers.Lookups
{
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Presentation;

    public class DictionaryLookup : DataProvider
    {
        #region Fields

        public DataBinding Dictionary;

        public DataBinding Key;

        private DataDictionary dataDictionary;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                if (this.dataDictionary == null)
                {
                    return null;
                }

                var key = this.Key.Value;

                object value = null;
                if (this.dataDictionary.Contains(key))
                {
                    value = this.dataDictionary[key];
                }
                return value;
            }
        }

        private DataDictionary DataDictionary
        {
            get
            {
                return this.dataDictionary;
            }
            set
            {
                if (value == this.dataDictionary)
                {
                    return;
                }

                if (this.dataDictionary != null)
                {
                    this.dataDictionary.CollectionChanged -= this.OnDataDictionaryChanged;
                }

                this.dataDictionary = value;

                if (this.dataDictionary != null)
                {
                    this.dataDictionary.CollectionChanged += this.OnDataDictionaryChanged;
                }
            }
        }

        #endregion

        #region Methods

        protected void Awake()
        {
            this.AddBinding(this.Key);
            this.AddBinding(this.Dictionary);
        }

        protected void OnDestroy()
        {
            this.RemoveBinding(this.Key);
            this.RemoveBinding(this.Dictionary);
        }

        protected void OnDisable()
        {
            this.Dictionary.ValueChanged -= this.OnDictionaryChanged;
        }

        protected void OnEnable()
        {
            this.Dictionary.ValueChanged += this.OnDictionaryChanged;
            this.DataDictionary = this.Dictionary.GetValue<DataDictionary>();
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        private void OnDataDictionaryChanged()
        {
            this.UpdateValue();
        }

        private void OnDictionaryChanged(object newValue)
        {
            this.DataDictionary = this.Dictionary.GetValue<DataDictionary>();
        }

        #endregion
    }
}