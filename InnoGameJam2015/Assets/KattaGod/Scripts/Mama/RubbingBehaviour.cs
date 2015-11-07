namespace KattaGod.Mama
{
    using System;

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class RubbingBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Fields

        /// <summary>
        ///   Maximum distance buffer to pile up.
        ///   This prevents that the rubbing doesn't end when dragging was already stopped.
        /// </summary>
        public float MaxBuffer = 0.0f;

        /// <summary>
        ///   Minimum dragging speed to trigger rubbing.
        /// </summary>
        public float MinimumSpeed = 0.0f;

        private float dragDistance;

        private float dragStartTime;

        private bool isDragging;

        private bool wasRubbingEnded;

        private bool wasRubbingStarted;

        #endregion

        #region Constructors and Destructors

        public RubbingBehaviour()
        {
            this.OnStart = new UnityEvent();
            this.OnEnd = new UnityEvent();
        }

        #endregion

        #region Properties

        public UnityEvent OnEnd { get; private set; }

        public UnityEvent OnStart { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.isDragging = true;
            this.dragDistance = 0;
            this.dragStartTime = Time.time;
            this.wasRubbingStarted = false;
            this.wasRubbingEnded = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            this.dragDistance += eventData.delta.magnitude;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.isDragging = false;
            if (!this.wasRubbingEnded)
            {
                this.OnRubbingEnd();
                this.wasRubbingEnded = true;
            }
        }

        #endregion

        #region Methods

        protected void Update()
        {
            // Check if dragging.
            if (this.isDragging)
            {
                // Check if minimum speed reached.
                var dragDuration = Time.time - this.dragStartTime;
                if (dragDuration > 0)
                {
                    var dragSpeed = this.dragDistance / dragDuration;
                    if (dragSpeed >= this.MinimumSpeed)
                    {
                        if (!this.wasRubbingStarted)
                        {
                            this.OnRubbingStart();
                            this.wasRubbingStarted = true;
                        }
                    }
                    else
                    {
                        if (this.wasRubbingStarted && !this.wasRubbingEnded)
                        {
                            this.OnRubbingEnd();
                            this.wasRubbingEnded = true;
                        }
                    }

                    // Cut buffer.
                    this.dragDistance = Math.Min(this.MinimumSpeed * dragDuration + this.MaxBuffer, this.dragDistance);
                }
            }
        }

        private void OnRubbingEnd()
        {
            Debug.Log("Rubbing end");
            this.OnEnd.Invoke();
        }

        private void OnRubbingStart()
        {
            Debug.Log("Rubbing start");
            this.OnStart.Invoke();
        }

        #endregion
    }
}