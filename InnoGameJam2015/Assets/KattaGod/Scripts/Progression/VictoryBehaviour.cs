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

        #endregion

        #region Constructors and Destructors

        public VictoryBehaviour()
        {
            this.Satisfaction = Satisfaction.Neutral;
        }

        #endregion

        #region Events

        public event Action Defeat;

        #endregion

        #region Properties

        public Satisfaction Satisfaction { get; private set; }

        #endregion

        #region Methods

        protected void OnDisable()
        {
            if (this.Orders != null)
            {
                this.Orders.OrderExpired -= this.OnOrderExpired;
                this.Orders.OrderFulfilled -= this.OnOrderFulfilled;
                this.Orders.OrderFailed -= this.OnOrderFailed;
            }
        }

        protected void OnEnable()
        {
            if (this.Orders != null)
            {
                this.Orders.OrderExpired += this.OnOrderExpired;
                this.Orders.OrderFulfilled += this.OnOrderFulfilled;
                this.Orders.OrderFailed += this.OnOrderFailed;
            }
        }

        private void DecreaseSatisfaction()
        {
            // Check if loosing game.
            if (this.Satisfaction == Satisfaction.VeryAngry)
            {
                this.OnDefeat();
                return;
            }

            // Decrease satisfaction.
            this.SetSatisfaction(this.Satisfaction - 1);
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
            this.DecreaseSatisfaction();
        }

        private void OnOrderFailed(OrdersBehaviour.Order order)
        {
            this.DecreaseSatisfaction();
        }

        private void OnOrderFulfilled(OrdersBehaviour.Order order)
        {
            // Check if already super happy.
            if (this.Satisfaction == Satisfaction.VeryHappy)
            {
                return;
            }

            // Increase satisfaction.
            this.SetSatisfaction(this.Satisfaction + 1);
        }

        private void SetSatisfaction(Satisfaction newSatisfaction)
        {
            this.Satisfaction = newSatisfaction;
            if (this.VictoryContext != null)
            {
                this.VictoryContext.Satisfaction = this.Satisfaction;
            }
        }

        #endregion
    }
}