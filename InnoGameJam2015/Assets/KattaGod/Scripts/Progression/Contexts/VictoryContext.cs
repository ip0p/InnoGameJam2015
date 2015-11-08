namespace KattaGod.Progression.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class VictoryContext : Context
    {
        #region Fields

        private readonly Property<bool> orderFailedProperty = new Property<bool>();

        private readonly Property<bool> orderFulfilledProperty = new Property<bool>();

        private readonly Property<Satisfaction> satisfactionProperty = new Property<Satisfaction>();

        #endregion

        #region Properties

        public bool OrderFailed
        {
            get
            {
                return this.orderFailedProperty.Value;
            }
            set
            {
                this.orderFailedProperty.Value = value;
            }
        }

        public bool OrderFulfilled
        {
            get
            {
                return this.orderFulfilledProperty.Value;
            }
            set
            {
                this.orderFulfilledProperty.Value = value;
            }
        }

        public Satisfaction Satisfaction
        {
            get
            {
                return this.satisfactionProperty.Value;
            }
            set
            {
                this.satisfactionProperty.Value = value;
            }
        }

        #endregion
    }
}