namespace KattaGod.Mama
{
    using System;

    using KattaGod.Mama.Contexts;

    using UnityEngine;
    
    public class MamaBehaviour : MonoBehaviour
    {
        #region Fields

        public float RequiredDanceDuration = 2.0f;

        public float RequiredPraiseDuration = 2.0f;

        private MamaContext context;

        private float danceStartTime;

        private bool isDancing;

        private bool isPraising;

        private float praiseStartTime;

        #endregion

        #region Events

        public event Action DanceCompleted;

        public event Action PraiseCompleted;

        #endregion

        #region Public Methods and Operators

        public void Init(MamaContext mamaContext)
        {
            this.context = mamaContext;
            this.context.StartDancing += this.OnStartDancing;
            this.context.EndDancing += this.OnEndDancing;
            this.context.StartPraising += this.OnStartPraising;
            this.context.EndPraising += this.OnEndPraising;
        }

        #endregion

        #region Methods

        protected void Update()
        {
            // Check if dance should be triggered.
            if (this.isDancing)
            {
                var danceDuration = Time.time - this.danceStartTime;
                if (danceDuration > this.RequiredDanceDuration)
                {
                    this.OnDanceCompleted();
                    this.isDancing = false;
                }
            }

            // Check if praise should be triggered.
            if (this.isPraising)
            {
                var praiseDuration = Time.time - this.praiseStartTime;
                if (praiseDuration > this.RequiredPraiseDuration)
                {
                    this.OnPraiseCompleted();
                    this.isPraising = false;
                }
            }
        }

        private void OnDanceCompleted()
        {
            var handler = this.DanceCompleted;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnEndDancing()
        {
            this.isDancing = false;
        }

        private void OnEndPraising()
        {
            this.isPraising = false;
        }

        private void OnPraiseCompleted()
        {
            var handler = this.PraiseCompleted;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnStartDancing()
        {
            this.isDancing = true;
            this.danceStartTime = Time.time;
        }

        private void OnStartPraising()
        {
            this.isPraising = true;
            this.praiseStartTime = Time.time;
        }

        #endregion
    }
}