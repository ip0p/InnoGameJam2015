namespace KattaGod.Recipes.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class StepContext : Context
    {
        #region Fields

        private readonly Property<string> textProperty = new Property<string>();

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

        #endregion
    }
}