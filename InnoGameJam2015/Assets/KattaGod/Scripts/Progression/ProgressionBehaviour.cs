namespace KattaGod.Progression
{
    using KattaGod.Orders;

    using UnityEngine;

    public class ProgressionBehaviour : MonoBehaviour
    {
        #region Fields

        public Config Config;

        /// <summary>
        ///   Delay between two orders (in s).
        /// </summary>
        public float DelayBetweenOrders = 10.0f;

        public GameManager GameManager;

        public OrdersBehaviour Orders;

        /// <summary>
        ///   Time till next order (in s).
        /// </summary>
        public float TimeTillNextOrder;

        #endregion

        #region Methods

        protected void Update()
        {
            this.TimeTillNextOrder -= Time.deltaTime;
            if (this.TimeTillNextOrder <= 0)
            {
                this.CreateNewOrder();
                this.TimeTillNextOrder = this.DelayBetweenOrders;
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
            return this.Config.receiptBook[Random.Range(0, this.Config.receiptBook.Count)];
        }

        private void CreateNewOrder()
        {
            if (this.GameManager == null)
            {
                Debug.LogWarning("No game manager set");
                return;
            }
            if (this.Orders == null)
            {
                Debug.LogWarning("No orders set");
                return;
            }

            // Get random recipe.
            var recipe = this.ChooseRecipe();
            if (recipe == null)
            {
                Debug.LogWarning("Couldn't choose a recipe.");
                return;
            }

            // Add order.
            this.Orders.AddOrder(recipe, recipe.BaseDuration);
        }

        #endregion
    }
}