namespace KattaGod.World.Contexts
{
    using System;

    using KattaGod.Fire.Contexts;
    using KattaGod.Inventory.Contexts;
    using KattaGod.Orders.Contexts;
    using KattaGod.Progression.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    using UnityEngine;

    public class WorldContext : Context
    {
        #region Fields

        private readonly Property<FireContext> fireProperty = new Property<FireContext>();

        private readonly Property<OrdersContext> ordersProperty = new Property<OrdersContext>();

        private readonly Property<VictoryContext> victoryProperty = new Property<VictoryContext>();

        #endregion

        #region Events

        public event Action<ItemContext> DropItem;

        #endregion

        #region Properties

        public FireContext Fire
        {
            get
            {
                return this.fireProperty.Value;
            }
            set
            {
                this.fireProperty.Value = value;
            }
        }

        public OrdersContext Orders
        {
            get
            {
                return this.ordersProperty.Value;
            }
            set
            {
                this.ordersProperty.Value = value;
            }
        }

        public VictoryContext Victory
        {
            get
            {
                return this.victoryProperty.Value;
            }
            set
            {
                this.victoryProperty.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void DoDropItem(ItemContext item)
        {
            this.OnDropItem(item);
        }

        #endregion

        #region Methods

        private void OnDropItem(ItemContext item)
        {
            var handler = this.DropItem;
            if (handler != null)
            {
                handler(item);
            }
        }

        #endregion
    }
}