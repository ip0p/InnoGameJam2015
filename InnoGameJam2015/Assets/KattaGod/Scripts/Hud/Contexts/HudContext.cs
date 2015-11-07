namespace KattaGod.Hud.Contexts
{
    using KattaGod.Cookbook.Contexts;
    using KattaGod.Inventory.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    public class HudContext : Context
    {
        #region Fields

        private readonly Property<CookbookContext> cookbookProperty = new Property<CookbookContext>();

        private readonly Property<InventoryContext> inventoryProperty = new Property<InventoryContext>();

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

        #endregion
    }
}