namespace KattaGod.Mama
{
    using Slash.Unity.DataBind.UI.Unity.Commands;

    using UnityEngine.Events;

    public class RubbingEndCommand : UnityEventCommand<RubbingBehaviour>
    {
        #region Methods

        protected override UnityEvent GetEvent(RubbingBehaviour target)
        {
            return target.OnEnd;
        }

        #endregion
    }
}