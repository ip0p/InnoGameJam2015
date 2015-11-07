namespace KattaGod.Progression
{
    using System;

    using KattaGod.Orders;
    using KattaGod.Progression.Contexts;

    using UnityEngine;

    public enum Satisfaction
    {
        Invalid,

        VeryAngry,

        Angry,

        Neutral,

        Happy,

        VeryHappy,
    }

    public class VictoryBehaviour : MonoBehaviour
    {
        #region Fields

        public OrdersBehaviour Orders;

        /// <summary>
        ///   Context to manage.
        /// </summary>
        public VictoryContext VictoryContext;

        private Satisfaction satisfaction = Satisfaction.Neutral;

        #endregion

        #region Events

        public event Action Defeat;

        #endregion

        #region Methods

        protected void OnDisable()
        {
            if (this.Orders != null)
            {
                this.Orders.OrderExpired -= this.OnOrderExpired;
                this.Orders.OrderFulfilled -= this.OnOrderFulfilled;
            }
        }

        protected void OnEnable()
        {
            if (this.Orders != null)
            {
                this.Orders.OrderExpired += this.OnOrderExpired;
                this.Orders.OrderFulfilled += this.OnOrderFulfilled;
            }
        }

        private void OnDefeat()
        {
            var handler = this.Defeat;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnOrderExpired(OrdersBehaviour.Order order)
        {
            // Check if loosing game.
            if (this.satisfaction == Satisfaction.VeryAngry)
            {
                this.OnDefeat();
                return;
            }

            // Decrease satisfaction.
            this.SetSatisfaction(this.satisfaction - 1);
        }

        private void OnOrderFulfilled(OrdersBehaviour.Order order)
        {
            // Check if already super happy.
            if (this.satisfaction == Satisfaction.VeryHappy)
            {
                return;
            }

            // Increase satisfaction.
            this.SetSatisfaction(this.satisfaction + 1);
        }

        private void SetSatisfaction(Satisfaction newSatisfaction)
        {
            this.satisfaction = newSatisfaction;
            if (this.VictoryContext != null)
            {
                this.VictoryContext.Satisfaction = this.satisfaction;
            }
        }

        #endregion
    }
}