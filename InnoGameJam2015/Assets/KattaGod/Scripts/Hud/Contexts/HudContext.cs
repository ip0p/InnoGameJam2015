namespace KattaGod.Hud.Contexts
{
    using KattaGod.Cookbook.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    public class HudContext : Context
    {
        #region Fields

        private readonly Property<CookbookContext> cookbookProperty = new Property<CookbookContext>();

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

        #endregion
    }
}