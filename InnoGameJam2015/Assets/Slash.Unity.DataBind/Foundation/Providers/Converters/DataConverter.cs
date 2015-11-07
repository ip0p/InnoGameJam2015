namespace Slash.Unity.DataBind.Foundation.Providers.Converters
{
    using Slash.Unity.DataBind.Core.Presentation;

    public abstract class DataConverter<TFrom, TTo> : DataProvider
    {
        #region Fields

        /// <summary>
        ///   Data value to use.
        /// </summary>
        public DataBinding Data;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var value = this.Data.GetValue<TFrom>();
                return this.Convert(value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected virtual void Awake()
        {
            this.AddBinding(this.Data);
        }

        protected abstract TTo Convert(TFrom value);

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}