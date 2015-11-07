namespace KattaGod.Recipes.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class StepContext : Context
    {
        #region Fields

        private readonly Property<string> textProperty = new Property<string>();

        private readonly Property<Ingredient.type> typeProperty = new Property<Ingredient.type>();

        #endregion

        #region Properties

        public string Text
        {
            get
            {
                return this.textProperty.Value;
            }
            set
            {
                this.textProperty.Value = value;
            }
        }

        public Ingredient.type Type
        {
            get
            {
                return this.typeProperty.Value;
            }
            set
            {
                this.typeProperty.Value = value;
            }
        }

        #endregion
    }
}