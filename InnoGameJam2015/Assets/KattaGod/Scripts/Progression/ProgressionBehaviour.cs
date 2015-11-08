namespace KattaGod.Progression
{
    using System;
    using System.Linq;

    using KattaGod.Orders;

    using UnityEngine;

    using Random = UnityEngine.Random;

    public class ProgressionBehaviour : MonoBehaviour
    {
        #region Fields

        public Config Config;

        /// <summary>
        ///   Ratio of duration to grant the player.
        /// </summary>
        public float DurationRatio = 1.0f;

        /// <summary>
        ///   Decrement of duration ratio per fulfilled order.
        /// </summary>
        public float DurationRatioDecrement = 0.01f;

        public GameManager GameManager;

        public OrdersBehaviour Orders;

        /// <summary>
        ///   Time till next order (in s).
        /// </summary>
        public float TimeTillNextOrder;

        public float TimeTillNextOrderWhenEmpty = 0.5f;

        private int numFulfilledOrders;

        #endregion

        #region Methods

        protected void OnDisable()
        {
            if (this.Orders != null)
            {
                this.Orders.OrderFulfilled -= this.OnOrderFulfilled;
                this.Orders.OrderRemoved -= this.OnOrderRemoved;
            }
        }

        protected void OnEnable()
        {
            if (this.Orders != null)
            {
                this.Orders.OrderFulfilled += this.OnOrderFulfilled;
                this.Orders.OrderRemoved += this.OnOrderRemoved;
            }
        }

        protected void Update()
        {
            this.TimeTillNextOrder -= Time.deltaTime;
            if (this.TimeTillNextOrder <= 0)
            {
                var duration = this.CreateNewOrder();
                this.TimeTillNextOrder = duration * 0.9f;
            }
        }

        private Receipt ChooseRecipe()
        {
            if (this.Config == null)
            {
                Debug.LogWarning("No config set.");
                return null;
            }
            if (this.Config.receiptBook.Count == 0)
            {
                Debug.LogWarning("No recipes set in config");
                return null;
            }

            // Order recipes by difficulty.
            var maxDifficulty = Math.Min(this.Config.receiptBook.Count, this.numFulfilledOrders + 1);
            return this.Config.receiptBook.OrderBy(recipe => recipe.Difficulty).ToList()[Random.Range(0, maxDifficulty)];
        }

        private float CreateNewOrder()
        {
            if (this.GameManager == null)
            {
                Debug.LogWarning("No game manager set");
                return 0;
            }
            if (this.Orders == null)
            {
                Debug.LogWarning("No orders set");
                return 0;
            }

            // Get random recipe.
            var recipe = this.ChooseRecipe();
            if (recipe == null)
            {
                Debug.LogWarning("Couldn't choose a recipe.");
                return 0;
            }

            // Add order.
            var duration = recipe.BaseDuration * this.DurationRatio;
            this.Orders.AddOrder(recipe, duration);

            return duration;
        }

        private void OnOrderFulfilled(OrdersBehaviour.Order order)
        {
            ++this.numFulfilledOrders;
            this.DurationRatio -= this.DurationRatioDecrement;
        }

        private void OnOrderRemoved(OrdersBehaviour.Order order)
        {
            // Check if orders left.
            if (this.Orders.Orders.Count == 0)
            {
                this.TimeTillNextOrder = Math.Min(this.TimeTillNextOrder, this.TimeTillNextOrderWhenEmpty);
            }
        }

        #endregion
    }
}