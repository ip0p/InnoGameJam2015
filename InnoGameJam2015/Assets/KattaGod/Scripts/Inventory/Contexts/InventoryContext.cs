namespace KattaGod.Inventory.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class InventoryContext : Context
    {
        #region Fields

        private readonly Property<Collection<ItemContext>> itemsProperty =
            new Property<Collection<ItemContext>>(new Collection<ItemContext>());

        #endregion

        #region Properties

        public Collection<ItemContext> Items
        {
            get
            {
                return this.itemsProperty.Value;
            }
            set
            {
                this.itemsProperty.Value = value;
            }
        }

        #endregion
    }
}