namespace KattaGod.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using KattaGod.Cookbook.Contexts;
    using KattaGod.Core.Contexts;
    using KattaGod.Fire;
    using KattaGod.Fire.Contexts;
    using KattaGod.Hud.Contexts;
    using KattaGod.Inventory.Contexts;
    using KattaGod.Orders;
    using KattaGod.Orders.Contexts;
    using KattaGod.Progression;
    using KattaGod.Progression.Contexts;
    using KattaGod.Recipes.Contexts;
    using KattaGod.World.Contexts;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    public class Initializer : MonoBehaviour
    {
        #region Fields

        public Config Config;

        public FireBehaviour Fire;

        public GameManager GameManager;

        public string HudScene = "Hud";

        public OrdersBehaviour Orders;

        public VictoryBehaviour Victory;

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

            var fireContext = new FireContext();
            if (this.Fire != null)
            {
                this.Fire.Init(fireContext);
                this.Fire.BurnCompleted += this.OnBurnCompleted;
            }

            var victoryContext = new VictoryContext();
            if (this.Victory != null)
            {
                victoryContext.Satisfaction = this.Victory.Satisfaction;
                this.Victory.VictoryContext = victoryContext;
                this.Victory.Defeat += this.OnDefeat;
            }

            // Create contexts.
            this.gameContext = new GameContext
            {
                Inventory = this.CreateInventory(),
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
            var worldContext = new WorldContext { Orders = this.gameContext.Orders, Victory = victoryContext, Fire = fireContext };
            worldContext.DropItem += this.OnDropItem;
            this.AddScene(this.WorldScene, worldContext);

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

            // Try to get special objects from scene.
            if (this.GameManager != null)
            {
                var altarGameObject = GameObject.FindGameObjectWithTag("Altar");
                if (altarGameObject != null)
                {
                    this.GameManager.altar = altarGameObject;
                }
            }
        }

        private InventoryContext CreateInventory()
        {
            var inventoryContext = new InventoryContext();

            // Add items for ingredients.
            inventoryContext.Items.Add(this.CreateItem(Ingredient.type.Grub));
            inventoryContext.Items.Add(this.CreateItem(Ingredient.type.Candle));
            inventoryContext.Items.Add(this.CreateItem(Ingredient.type.Corn));

            return inventoryContext;
        }

        private ItemContext CreateItem(Ingredient.type ingredientType)
        {
            return new ItemContext()
            {
                IngredientType = ingredientType,
                Icon = ingredientType.ToString(),
                Name = ingredientType.ToString()
            };
        }

        private RecipeContext CreateRecipe(Receipt recipe)
        {
            var recipeContext = new RecipeContext() { Id = recipe.ID, Title = recipe.ID };
            foreach (var ingredient in recipe.Ingredients)
            {
                recipeContext.Steps.Add(
                    new StepContext() { Type = ingredient.CurrentType, Text = ingredient.CurrentType.ToString() });
            }
            return recipeContext;
        }

        private OrderContext GetOrderContext(OrdersBehaviour.Order order)
        {
            return order != null
                ? this.gameContext.Orders.Orders.FirstOrDefault(orderContext => orderContext.Id == order.Id)
                : null;
        }

        private RecipeContext GetRecipeContext(Receipt recipe)
        {
            return this.recipes.FirstOrDefault(recipeContext => recipeContext.Id == recipe.ID);
        }

        private void OnBurnCompleted()
        {
            if (this.GameManager != null && this.GameManager.CurrentReceipt != null)
            {
                this.GameManager.AddIngredient(Ingredient.type.Fire);
            }
        }

        private void OnDefeat()
        {
            Debug.Log("DEFEAT");
        }

        private void OnDropItem(ItemContext item)
        {
            // Check if active recipe.
            if (this.GameManager.CurrentReceipt == null)
            {
                Debug.Log("No active recipe.");
                return;
            }

            this.GameManager.AddIngredient(item.IngredientType);
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
            // Set recipe of order to game manager.
            this.GameManager.CurrentReceipt = order != null ? order.Recipe : null;

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