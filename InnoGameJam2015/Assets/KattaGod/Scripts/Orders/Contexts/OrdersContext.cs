namespace KattaGod.Orders.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class OrdersContext : Context
    {
        #region Fields

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