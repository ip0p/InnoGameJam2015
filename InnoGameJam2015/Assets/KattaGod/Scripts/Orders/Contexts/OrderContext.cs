namespace KattaGod.Orders.Contexts
{
    using KattaGod.Recipes.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    public class OrderContext : Context
    {
        #region Fields

        private readonly Property<bool> isActiveProperty = new Property<bool>();

        private readonly Property<RecipeContext> recipeProperty = new Property<RecipeContext>();

        private readonly Property<float> remainingDurationProperty = new Property<float>();

        #endregion

        #region Properties

        public uint Id { get; set; }

        public bool IsActive
        {
            get
            {
                return this.isActiveProperty.Value;
            }
            set
            {
                this.isActiveProperty.Value = value;
            }
        }

        public RecipeContext Recipe
        {
            get
            {
                return this.recipeProperty.Value;
            }
            set
            {
                this.recipeProperty.Value = value;
            }
        }

        public float RemainingDuration
        {
            get
            {
                return this.remainingDurationProperty.Value;
            }
            set
            {
                this.remainingDurationProperty.Value = value;
            }
        }

        #endregion
    }
}