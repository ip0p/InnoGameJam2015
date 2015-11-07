namespace Slash.Unity.DataBind.Foundation.Setters
{
    using UnityEngine;

    public class BehaviourEnabledSetter : ComponentSingleSetter<Behaviour, bool>
    {
        protected override void OnValueChanged(bool newValue)
        {
            this.Target.enabled = newValue;
        }
    }
}