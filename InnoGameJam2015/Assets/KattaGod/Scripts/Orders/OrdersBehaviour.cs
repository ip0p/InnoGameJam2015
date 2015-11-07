namespace KattaGod.Orders
{
    using System;
    using System.Collections.Generic;

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

        public event Action<Order> OrderRemoved;

        #endregion

        #region Public Methods and Operators

        public void AddOrder(Receipt recipe, float duration)
        {
            var order = new Order() { Recipe = recipe, RemainingDuration = duration, Id = this.nextId++ };
            this.Orders.Add(order);
            this.OnOrderAdded(order);
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
                order.RemainingDuration -= Time.deltaTime;
                if (order.RemainingDuration <= 0)
                {
                    this.OnOrderExpired(order);
                    this.Orders.RemoveAt(index);
                    this.OnOrderRemoved(order);
                }
            }
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