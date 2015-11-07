namespace KattaGod.Mama
{
    using Slash.Unity.DataBind.UI.Unity.Commands;

    using UnityEngine.Events;

    public class RubbingStartCommand : UnityEventCommand<RubbingBehaviour>
    {
        #region Methods

        protected override UnityEvent GetEvent(RubbingBehaviour target)
        {
            return target.OnStart;
        }

        #endregion
    }
}