namespace KattaGod.Mama.Contexts
{
    using System;

    using Slash.Unity.DataBind.Core.Data;

    public class MamaContext : Context
    {
        #region Fields

        private readonly Property<bool> isDancingProperty = new Property<bool>();

        private readonly Property<bool> isPraisingProperty = new Property<bool>();

        #endregion

        #region Delegates

        public delegate void EndDancingDelegate();

        public delegate void StartDancingDelegate();

        #endregion

        #region Events

        public event StartDancingDelegate StartDancing;

        public event EndDancingDelegate EndDancing;

        public event Action StartPraising;

        public event Action EndPraising;

        #endregion

        #region Properties

        public bool IsDancing
        {
            get
            {
                return this.isDancingProperty.Value;
            }
            set
            {
                this.isDancingProperty.Value = value;
            }
        }

        public bool IsPraising
        {
            get
            {
                return this.isPraisingProperty.Value;
            }
            set
            {
                this.isPraisingProperty.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void DoEndDancing()
        {
            this.IsDancing = false;
            this.OnEndDancing();
        }

        public void DoEndPraising()
        {
            this.IsPraising = false;
            this.OnEndPraising();
        }

        public void DoStartDancing()
        {
            this.IsDancing = true;
            this.OnStartDancing();
        }

        public void DoStartPraising()
        {
            this.IsPraising = true;
            this.OnStartPraising();
        }

        #endregion

        #region Methods

        private void OnEndDancing()
        {
            var handler = this.EndDancing;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnEndPraising()
        {
            var handler = this.EndPraising;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnStartDancing()
        {
            var handler = this.StartDancing;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnStartPraising()
        {
            var handler = this.StartPraising;
            if (handler != null)
            {
                handler();
            }
        }

        #endregion
    }
}