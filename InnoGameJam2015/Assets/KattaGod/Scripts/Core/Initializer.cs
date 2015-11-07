namespace KattaGod.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using KattaGod.Cookbook.Contexts;
    using KattaGod.Core.Contexts;
    using KattaGod.Hud.Contexts;
    using KattaGod.Inventory.Contexts;
    using KattaGod.Orders;
    using KattaGod.Orders.Contexts;
    using KattaGod.Recipes.Contexts;
    using KattaGod.World.Contexts;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    public class Initializer : MonoBehaviour
    {
        #region Fields

        public Config Config;

        public GameManager GameManager;

        public string HudScene = "Hud";

        public OrdersBehaviour Orders;

        public string WorldScene = "World";

        private GameContext gameContext;

        private List<RecipeContext> recipes;

        #endregion

        #region Public Methods and Operators

        public void Start()
        {
            // Register for game events.
            if (this.Orders != null)
            {
                this.Orders.OrderAdded += this.OnOrderAdded;
                this.Orders.OrderRemoved += this.OnOrderRemoved;
                this.Orders.OrderSelected += this.OnOrderSelected;
            }

            // Create contexts.
            this.gameContext = new GameContext
            {
                Inventory = new InventoryContext(),
                Cookbook = new CookbookContext(),
                Orders = new OrdersContext()
            };

            this.recipes = new List<RecipeContext>();
            if (this.Config != null)
            {
                foreach (var recipe in this.Config.receiptBook)
                {
                    this.recipes.Add(this.CreateRecipe(recipe));
                }
            }

            // Register for commands.
            this.gameContext.Orders.SelectOrder += this.OnSelectOrder;

            // Load world.
            this.AddScene(this.WorldScene, new WorldContext() { Orders = this.gameContext.Orders });

            // Load hud.
            this.AddScene(
                this.HudScene,
                new HudContext() { Cookbook = this.gameContext.Cookbook, Inventory = this.gameContext.Inventory });
        }

        #endregion

        #region Methods

        private void AddScene(string sceneName, object context)
        {
            this.StartCoroutine(this.AddSceneAsync(sceneName, context));
        }

        private IEnumerator AddSceneAsync(string sceneName, object context)
        {
            yield return Application.LoadLevelAdditiveAsync(sceneName);

            yield return new WaitForEndOfFrame();

            var rootGameObject = GameObject.Find(sceneName);
            if (rootGameObject != null)
            {
                var contextHolder = rootGameObject.GetComponent<ContextHolder>();
                if (contextHolder != null)
                {
                    contextHolder.Context = context;
                }
            }
        }

        private RecipeContext CreateRecipe(Receipt recipe)
        {
            var recipeContext = new RecipeContext() { Id = recipe.ID, Title = recipe.ID };
            foreach (var ingredient in recipe.Ingredients)
            {
                recipeContext.Steps.Add(
                    new StepContext() { Type = ingredient.Currenttype, Text = ingredient.Currenttype.ToString() });
            }
            return recipeContext;
        }

        private OrderContext GetOrderContext(OrdersBehaviour.Order order)
        {
            return this.gameContext.Orders.Orders.FirstOrDefault(orderContext => orderContext.Id == order.Id);
        }

        private RecipeContext GetRecipeContext(Receipt recipe)
        {
            return this.recipes.FirstOrDefault(recipeContext => recipeContext.Id == recipe.ID);
        }

        private void OnOrderAdded(OrdersBehaviour.Order order)
        {
            var recipeContext = this.GetRecipeContext(order.Recipe);
            var orderContext = new OrderContext()
            {
                Id = order.Id,
                Recipe = recipeContext,
                RemainingDuration = order.RemainingDuration
            };
            this.gameContext.Orders.Orders.Add(orderContext);
        }

        private void OnOrderRemoved(OrdersBehaviour.Order order)
        {
            var orderContext = this.GetOrderContext(order);
            if (orderContext != null)
            {
                this.gameContext.Orders.Orders.Remove(orderContext);
            }
        }

        private void OnOrderSelected(OrdersBehaviour.Order order)
        {
            // TODO(co): Set recipe of order to game manager.

            var orderContext = this.GetOrderContext(order);
            if (orderContext != null)
            {
                orderContext.IsActive = true;
            }

            this.gameContext.Cookbook.Recipe = orderContext != null ? orderContext.Recipe : null;
        }

        private void OnSelectOrder(OrderContext order)
        {
            Debug.Log("Select order: " + order);

            // Check if an order is already selected.
            if (this.Orders.SelectedOrder != null)
            {
                Debug.Log("Other order already selected.");
                return;
            }

            this.Orders.SelectOrder(order.Id);
        }

        #endregion
    }
}