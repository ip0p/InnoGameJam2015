namespace KattaGod.Orders.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class OrdersContext : Context
    {
        #region Fields

        private readonly Property<Collection<OrderContext>> ordersProperty =
            new Property<Collection<OrderContext>>(new Collection<OrderContext>());

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
    }
}