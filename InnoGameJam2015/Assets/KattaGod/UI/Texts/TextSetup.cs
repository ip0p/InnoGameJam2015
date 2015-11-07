namespace UnityApplication.View.Texts
{
    using UnityEngine;

    public class TextSetup : MonoBehaviour
    {
        #region Fields

        public string Style;

        #endregion

        #region Public Methods and Operators

        [ContextMenu("Apply")]
        public void Apply()
        {
            TextSettings.Instance.ApplyStyle(this.gameObject, this.Style);
        }

        #endregion
    }
}