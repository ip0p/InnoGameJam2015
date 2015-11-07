namespace KattaGod.World.Contexts
{
    using KattaGod.Inventory.Contexts;
    using KattaGod.Orders.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    using UnityEngine;

    public class WorldContext : Context
    {
        #region Fields

        private readonly Property<OrdersContext> ordersProperty = new Property<OrdersContext>();

        #endregion

        #region Properties

        public OrdersContext Orders
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

        public void DoDropItem(ItemContext item)
        {
            Debug.Log("Dropped item " + item);
        }

        #endregion
    }
}