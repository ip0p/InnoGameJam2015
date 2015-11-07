namespace KattaGod.Cookbook.Contexts
{
    using KattaGod.Recipes.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    public class CookbookContext : Context
    {
        #region Fields

        private readonly Property<RecipeContext> recipeProperty = new Property<RecipeContext>();

        #endregion

        #region Properties

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

        #endregion
    }
}