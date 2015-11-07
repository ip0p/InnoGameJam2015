namespace KattaGod.Fire
{
    using System;

    using KattaGod.Fire.Contexts;

    using UnityEngine;

    public class FireBehaviour : MonoBehaviour
    {
        private FireContext context;

        private bool isBurning;

        private float burnStartTime;

        public event Action BurnCompleted;

        /// <summary>
        ///   Required duration to trigger burn step.
        /// </summary>
        public float RequiredBurnDuration = 2.0f;

        public void Init(FireContext fireContext)
        {
            this.context = fireContext;

            this.context.StartBurn += this.OnStartBurn;
            this.context.EndBurn += this.OnEndBurn;
        }

        protected void Update()
        {
            // Check if burn duration reached.
            if (this.isBurning)
            {
                var burnDuration = Time.time - this.burnStartTime;
                if (burnDuration > RequiredBurnDuration)
                {
                    this.OnBurnCompleted();

                    // Turn off burning logically.
                    this.isBurning = false;
                }
            }
        }

        private void OnBurnCompleted()
        {
            var handler = this.BurnCompleted;
            if (handler != null)
            {
                handler();
            }
        }

        private void OnEndBurn()
        {
            if (!this.isBurning)
            {
                return;
            }

            this.isBurning = false;
        }

        private void OnStartBurn()
        {
            if (this.isBurning)
            {
                return;
            }

            this.isBurning = true;
            this.burnStartTime = Time.time;
        }
    }
}