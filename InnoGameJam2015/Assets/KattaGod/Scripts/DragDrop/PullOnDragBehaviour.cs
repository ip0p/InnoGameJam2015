namespace KattaGod.DragDrop
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class PullOnDragBehaviour : MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IPointerDownHandler
    {
        #region Fields

        private Vector2 dragOffset;

        private Vector3 initialPosition;

        #endregion

        #region Public Methods and Operators

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.initialPosition = this.transform.position;
            this.dragOffset = new Vector2(this.initialPosition.x, this.initialPosition.y) - eventData.pressPosition;

            var canvasGroup = this.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            this.transform.position = eventData.position + this.dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.ResetPosition();

            var canvasGroup = this.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Only here to catch pointer down events and not let them fall through.
        }

        public void ResetPosition()
        {
            this.transform.position = this.initialPosition;
        }

        #endregion
    }
}