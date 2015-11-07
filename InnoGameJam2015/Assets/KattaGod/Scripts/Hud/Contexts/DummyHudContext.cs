namespace KattaGod.Hud.Contexts
{
    using KattaGod.Cookbook.Contexts;
    using KattaGod.Inventory.Contexts;
    using KattaGod.Recipes.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    public class DummyHudContext : HudContext

    {
        #region Constructors and Destructors

        public DummyHudContext()
        {
            this.Cookbook = new CookbookContext
            {
                Recipe =
                    new RecipeContext
                    {
                        Title = "The complete sacrifice",
                        Steps =
                            new Collection<StepContext>()
                            {
                                new StepContext() { Text = "Put a grub on the altar" },
                                new StepContext() { Text = "Roast the sacrifice" },
                                new StepContext() { Text = "Put corn on the altar" },
                                new StepContext() { Text = "Put corn on the altar" },
                                new StepContext() { Text = "Put a candle on the altar" },
                                new StepContext() { Text = "Put a candle on the altar" },
                                new StepContext() { Text = "Put a candle on the altar" },
                                new StepContext() { Text = "Put a candle on the altar" },
                                new StepContext() { Text = "Dance for me!" },
                                new StepContext() { Text = "Praise my power!" }
                            }
                    }
            };
            this.Inventory = new InventoryContext
            {
                Items =
                    new Collection<ItemContext>
                    {
                        new ItemContext() { Name = "Grub", Icon = "grub" },
                        new ItemContext() { Name = "Corn", Icon = "corn" },
                        new ItemContext() { Name = "Candle", Icon = "candle" }
                    }
            };
        }

        #endregion
    }
}