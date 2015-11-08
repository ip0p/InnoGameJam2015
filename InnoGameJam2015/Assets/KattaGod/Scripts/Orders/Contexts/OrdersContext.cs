namespace KattaGod.Orders.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class OrdersContext : Context
    {
        #region Fields

        private readonly Property<bool> isSelectedFailedProperty = new Property<bool>();

        private readonly Property<bool> isSelectedFulfilledProperty = new Property<bool>();

        private readonly Property<Collection<OrderContext>> ordersProperty =
            new Property<Collection<OrderContext>>(new Collection<OrderContext>());

        #endregion

        #region Delegates

        public delegate void SelectOrderDelegate(OrderContext order);

        #endregion

        #region Events

        public event SelectOrderDelegate SelectOrder;

        #endregion

        #region Properties

        public bool IsSelectedFailed
        {
            get
            {
                return this.isSelectedFailedProperty.Value;
            }
            set
            {
                this.isSelectedFailedProperty.Value = value;
            }
        }

        public bool IsSelectedFulfilled
        {
            get
            {
                return this.isSelectedFulfilledProperty.Value;
            }
            set
            {
                this.isSelectedFulfilledProperty.Value = value;
            }
        }

        public Collection<OrderContext> Orders
        {
            get
            {
                return this.ordersProperty.Value;
            }
            set
            {
                this.ordersProperty.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void DoSelectOrder(OrderContext order)
        {
            this.OnSelectOrder(order);
        }

        #endregion

        #region Methods

        private void OnSelectOrder(OrderContext order)
        {
            var handler = this.SelectOrder;
            if (handler != null)
            {
                handler(order);
            }
        }

        #endregion
    }
}