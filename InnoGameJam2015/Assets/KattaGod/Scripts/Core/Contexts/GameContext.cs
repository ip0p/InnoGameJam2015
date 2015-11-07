namespace KattaGod.Core.Contexts
{
    using KattaGod.Cookbook.Contexts;
    using KattaGod.Inventory.Contexts;
    using KattaGod.Orders.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    public class GameContext : Context
    {
        #region Fields

        private readonly Property<CookbookContext> cookbookProperty = new Property<CookbookContext>();

        private readonly Property<InventoryContext> inventoryProperty = new Property<InventoryContext>();

        private readonly Property<OrdersContext> ordersProperty = new Property<OrdersContext>();

        #endregion

        #region Properties

        public CookbookContext Cookbook
        {
            get
            {
                return this.cookbookProperty.Value;
            }
            set
            {
                this.cookbookProperty.Value = value;
            }
        }

        public InventoryContext Inventory
        {
            get
            {
                return this.inventoryProperty.Value;
            }
            set
            {
                this.inventoryProperty.Value = value;
            }
        }

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
    }
}