namespace KattaGod.World.Contexts
{
    using KattaGod.Orders.Contexts;
    using KattaGod.Recipes.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    public class DummyWorldContext : WorldContext
    {
        #region Constructors and Destructors

        public DummyWorldContext()
        {
            this.Orders = new OrdersContext
            {
                Orders =
                    new Collection<OrderContext>
                    {
                        new OrderContext() { IsActive = true, Recipe = new RecipeContext(), RemainingDuration = 5.0f },
                        new OrderContext() { Recipe = new RecipeContext(), RemainingDuration = 5.0f },
                        new OrderContext() { Recipe = new RecipeContext(), RemainingDuration = 15.0f }
                    }
            };
        }

        #endregion
    }
}