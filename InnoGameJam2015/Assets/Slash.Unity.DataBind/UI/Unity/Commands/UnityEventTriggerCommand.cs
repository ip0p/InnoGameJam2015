using System;
using Slash.Unity.DataBind.Core.Data;
using Slash.Unity.DataBind.Core.Presentation;
using Slash.Unity.DataBind.Core.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    /// <summary>
    ///   Command which is invoked when an event was triggered
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] UI Event Trigger Command (Unity)")]
    public class UnityEventTriggerCommand : MonoBehaviour, IBeginDragHandler, ICancelHandler, IDeselectHandler, IDragHandler,
        IDropHandler, IEndDragHandler, IMoveHandler, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, 
        IPointerExitHandler, IPointerUpHandler, IScrollHandler, ISelectHandler, ISubmitHandler, IUpdateSelectedHandler
    {
        #region CachedGameObject
        private GameObject localCachedGameObject;
        internal GameObject CachedGameObject
        {
            get
            {
                if (this.localCachedGameObject == null)
                    this.localCachedGameObject = this.gameObject;
                return this.localCachedGameObject;
            }
        }

        #endregion

        public EventTriggerBinding[] Events;

        public enum PossibleEvents
        {
            BeginDrag = 0,
            Cancel = 1,
            Deselect = 2,
            Drag = 3,
            Drop = 4,
            EndDrag = 5,
            Move = 6,
            PointerClick = 7,
            PointerDown = 8,
            PointerEnter = 9,
            PointerExit = 10,
            PointerUp = 11,
            Scroll = 12,
            Select = 13,
            Submit = 14,
            UpdateSelected = 15
        }

        #region event handler

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.BeginDrag, eventData);
        }

        public void OnCancel(BaseEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Cancel, eventData);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Deselect, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Drag, eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Drop, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.EndDrag, eventData);
        }

        public void OnMove(AxisEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Move, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.PointerClick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.PointerDown, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.PointerEnter, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.PointerExit, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.PointerUp, eventData);
        }

        public void OnScroll(PointerEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Scroll, eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Select, eventData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.Submit, eventData);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            this.ExecuteBindedFunction(PossibleEvents.UpdateSelected, eventData);
        }

        #endregion

        private void ExecuteBindedFunction(PossibleEvents triggeredEvent, BaseEventData eventData)
        {
            foreach (EventTriggerBinding eventBinding in this.Events)
            {
                if(eventBinding.Event == triggeredEvent)
                {
                    Context context = new ContextNode(this.gameObject, eventBinding.Path).Context as Context;
                    if (context != null)
                    {
                        Delegate pathDelegate = context.RegisterListener(eventBinding.Path, null) as Delegate;
                        if (pathDelegate != null)
                            pathDelegate.DynamicInvoke(this.CachedGameObject, eventData);
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class EventTriggerBinding
    {
        public UnityEventTriggerCommand.PossibleEvents Event;

        /// <summary>
        ///   Path of method to call in data context.
        /// </summary>
        [ContextPath(Filter = ContextMemberFilter.Methods | ContextMemberFilter.Recursive)]
        public string Path;
    }
}