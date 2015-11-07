namespace KattaGod.Inventory.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class ItemContext : Context
    {
        #region Fields

        private readonly Property<string> iconProperty = new Property<string>();

        private readonly Property<string> nameProperty = new Property<string>();

        #endregion

        #region Properties

        public string Icon
        {
            get
            {
                return this.iconProperty.Value;
            }
            set
            {
                this.iconProperty.Value = value;
            }
        }

        public Ingredient.type IngredientType { get; set; }

        public string Name
        {
            get
            {
                return this.nameProperty.Value;
            }
            set
            {
                this.nameProperty.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return string.Format("Name: {0}", this.Name);
        }

        #endregion
    }
}