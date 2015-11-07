namespace KattaGod.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;

    public class OrdersBehaviour : MonoBehaviour
    {
        #region Fields

        public List<Order> Orders;

        private uint nextId = 1;

        #endregion

        #region Events

        public event Action<Order> OrderAdded;

        public event Action<Order> OrderExpired;

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