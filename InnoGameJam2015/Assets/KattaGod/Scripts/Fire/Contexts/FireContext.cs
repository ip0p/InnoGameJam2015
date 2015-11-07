namespace KattaGod.Fire.Contexts
{
    using System;

    using Slash.Unity.DataBind.Core.Data;

    public class FireContext : Context
    {
        #region Fields

        private readonly Property<bool> isBurningProperty = new Property<bool>();

        #endregion

        #region Events

        public event Action StartBurn;

        public event Action EndBurn;

        #endregion

        #region Properties

        public bool IsBurning
        {
            get
            {
                return this.isBurningProperty.Value;
            }
            set
            {
                this.isBurningProperty.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void DoEndBurn()
        {
            this.IsBurning = false;
            this.OnEndBurn();
        }

        public void DoStartBurn()
        {
            this.IsBurning = true;
            this.OnStartBurn();
        }

        #endregion

        #region Methods

        private void OnEndBurn()
        {
            var handler = this.EndBurn;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnStartBurn()
        {
            var handler = this.StartBurn;
            if (handler != null)
            {
                handler();
            }
        }

        #endregion
    }
}