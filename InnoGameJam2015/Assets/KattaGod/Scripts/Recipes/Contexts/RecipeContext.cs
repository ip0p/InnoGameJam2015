namespace KattaGod.Recipes.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class RecipeContext : Context
    {
        #region Fields

        private readonly Property<Collection<StepContext>> stepsProperty =
            new Property<Collection<StepContext>>(new Collection<StepContext>());

        private readonly Property<string> titleProperty = new Property<string>();

        #endregion

        #region Properties

        public Collection<StepContext> Steps
        {
            get
            {
                return this.stepsProperty.Value;
            }
            set
            {
                this.stepsProperty.Value = value;
            }
        }

        private readonly Property<string> idProperty =
            new Property<string>();

        public string Id
        {
            get
            {
                return this.idProperty.Value;
            }
            set
            {
                this.idProperty.Value = value;
            }
        }

        public string Title
        {
            get
            {
                return this.titleProperty.Value;
            }
            set
            {
                this.titleProperty.Value = value;
            }
        }

        #endregion
    }
}