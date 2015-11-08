namespace KattaGod.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KattaGod.Orders.Contexts;

    using UnityEngine;

    public class OrdersBehaviour : MonoBehaviour
    {
        #region Fields

        public GameManager GameManager;

        public List<Order> Orders;

        private OrdersContext context;

        private uint nextId = 1;

        #endregion

        #region Events

        public event Action<Order> OrderAdded;

        public event Action<Order> OrderExpired;

        public event Action<Order> OrderFulfilled;

        public event Action<Order> OrderFailed;

        public event Action<Order> OrderSelected;

        public event Action<Order> OrderRemoved;

        #endregion

        #region Properties

        public Order SelectedOrder { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void AddOrder(Receipt recipe, float duration)
        {
            var order = new Order() { Recipe = recipe, RemainingDuration = duration, Id = this.nextId++ };
            this.Orders.Add(order);
            this.OnOrderAdded(order);
        }

        public void Init(OrdersContext ordersContext)
        {
            this.context = ordersContext;
        }

        public void SelectOrder(uint orderId)
        {
            // Check if another order is already selected.
            if (this.SelectedOrder != null)
            {
                throw new InvalidOperationException("Another order is already selected.");
            }

            // Get order with specified id.
            var order = this.Orders.FirstOrDefault(existingOrder => existingOrder.Id == orderId);
            if (order == null)
            {
                throw new ArgumentException("No order with id " + orderId + " found");
            }

            this.SelectedOrder = order;

            this.OnOrderSelected(this.SelectedOrder);
        }

        #endregion

        #region Methods

        protected void Awake()
        {
            this.Orders = new List<Order>();
        }

        protected void OnDisable()
        {
            if (this.GameManager != null)
            {
                this.GameManager.RecipeComplete -= this.OnRecipeCompleted;
                this.GameManager.RecipeFailed -= this.OnRecipeFailed;
            }
        }

        protected void OnEnable()
        {
            if (this.GameManager != null)
            {
                this.GameManager.RecipeComplete += this.OnRecipeCompleted;
                this.GameManager.RecipeFailed += this.OnRecipeFailed;
            }
        }

        protected void Update()
        {
            // Tick orders.
            for (var index = this.Orders.Count - 1; index >= 0; --index)
            {
                var order = this.Orders[index];

                // Don't tick selected order.
                if (order == this.SelectedOrder)
                {
                    continue;
                }

                order.RemainingDuration -= Time.deltaTime;
                if (order.RemainingDuration <= 0)
                {
                    this.OnOrderExpired(order);
                    this.Orders.RemoveAt(index);
                    this.OnOrderRemoved(order);
                }
            }

            if (this.context != null)
            {
                foreach (OrderContext orderContext in this.context.Orders)
                {
                    orderContext.RemainingDuration -= Time.deltaTime;
                }
            }
        }

        private void ClearSelectedOrder()
        {
            if (this.SelectedOrder == null)
            {
                return;
            }

            this.SelectedOrder = null;

            this.OnOrderSelected(null);
        }

        private void FailSelectedOrder()
        {
            if (this.SelectedOrder == null)
            {
                return;
            }

            this.OnOrderFailed(this.SelectedOrder);

            // Remove order.
            this.Orders.Remove(this.SelectedOrder);
            this.OnOrderRemoved(this.SelectedOrder);

            this.ClearSelectedOrder();
        }

        private void FulfillSelectedOrder()
        {
            if (this.SelectedOrder == null)
            {
                return;
            }

            this.OnOrderFulfilled(this.SelectedOrder);

            // Remove order.
            this.Orders.Remove(this.SelectedOrder);
            this.OnOrderRemoved(this.SelectedOrder);

            this.ClearSelectedOrder();
        }

        private void OnOrderAdded(Order order)
        {
            var handler = this.OrderAdded;
            if (handler != null)
            {
                handler(order);
            }
        }

        private void OnOrderExpired(Order order)
        {
            var handler = this.OrderExpired;
            if (handler != null)
            {
                handler(order);
            }
        }

        private void OnOrderFailed(Order order)
        {
            // Trigger order failed.
            if (this.context != null)
            {
                this.context.IsSelectedFailed = false;
                this.context.IsSelectedFailed = true;
            }

            var handler = this.OrderFailed;
            if (handler != null)
            {
                handler(order);
            }
        }

        private void OnOrderFulfilled(Order order)
        {
            // Trigger order fulfilled.
            if (this.context != null)
            {
                this.context.IsSelectedFulfilled = false;
                this.context.IsSelectedFulfilled = true;
            }

            var handler = this.OrderFulfilled;
            if (handler != null)
            {
                handler(order);
            }
        }

        private void OnOrderRemoved(Order order)
        {
            var handler = this.OrderRemoved;
            if (handler != null)
            {
                handler(order);
            }
        }

        private void OnOrderSelected(Order order)
        {
            var handler = this.OrderSelected;
            if (handler != null)
            {
                handler(order);
            }
        }

        private void OnRecipeCompleted(Receipt recipe)
        {
            // Check if selected order.
            if (this.SelectedOrder == null || this.SelectedOrder.Recipe != recipe)
            {
                return;
            }

            // Fulfill order.
            this.FulfillSelectedOrder();
        }

        private void OnRecipeFailed(Receipt recipe)
        {
            // Check if selected order.
            if (this.SelectedOrder == null || this.SelectedOrder.Recipe != recipe)
            {
                return;
            }

            // Fail order.
            this.FailSelectedOrder();
        }

        #endregion

        public class Order
        {
            #region Fields

            public Receipt Recipe;

            /// <summary>
            ///   Remaining duration this order is active (in s).
            /// </summary>
            public float RemainingDuration;

            #endregion

            #region Properties

            public uint Id { get; set; }

            #endregion
        }
    }
}