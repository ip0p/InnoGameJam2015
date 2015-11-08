namespace KattaGod.Core
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;

    public class PlaySound : ComponentSingleSetter<MonoBehaviour, bool>
    {
        #region Fields

        public AudioClip Clip;

        public AudioSource Source;

        #endregion

        #region Methods

        protected override void OnValueChanged(bool newValue)
        {
            if (newValue)
            {
                this.Play();
            }
            else
            {
                this.Stop();
            }
        }

        private void Play()
        {
            if (this.Source != null && this.Clip != null)
            {
                this.Source.clip = this.Clip;
                this.Source.loop = true;
                this.Source.Play();
            }
        }

        private void Stop()
        {
            if (this.Source != null)
            {
                this.Source.Stop();
            }
        }

        #endregion
    }
}