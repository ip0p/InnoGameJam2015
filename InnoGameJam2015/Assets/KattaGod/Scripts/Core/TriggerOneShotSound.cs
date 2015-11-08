namespace KattaGod.Core
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;

    public class TriggerOneShotSound : ComponentSingleSetter<MonoBehaviour, bool>
    {
        #region Fields

        public AudioClip Clip;

        public AudioSource Source;

        #endregion

        #region Public Methods and Operators

        public void Trigger()
        {
            if (this.Source != null && this.Clip != null)
            {
                this.Source.PlayOneShot(this.Clip);
            }
        }

        #endregion

        #region Methods

        protected override void OnValueChanged(bool newValue)
        {
            if (newValue)
            {
                this.Trigger();
            }
        }

        #endregion
    }
}