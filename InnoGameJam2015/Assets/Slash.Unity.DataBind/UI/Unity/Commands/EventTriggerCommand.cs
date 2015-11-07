namespace Slash.Unity.DataBind.UI.Unity.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    [AddComponentMenu("Data Bind/UnityUI/Commands/[DB] Event Trigger Command (Unity)")]
    public class EventTriggerCommand : UnityEventCommand<EventTrigger, BaseEventData>
    {
        #region Methods

        protected override IEnumerable<UnityEvent<BaseEventData>> GetEvents(EventTrigger target)
        {
            return target.triggers.Select(trigger => trigger.callback as UnityEvent<BaseEventData>);
        }

        #endregion
    }
}