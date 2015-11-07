namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    ///   Setter which sets enalbes an image component when given a sprite value.
    ///   <para>Input: <see cref="Sprite" /></para>
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Image Sprite Enabler (Unity)")]
    public class ImageSpriteEnabler : ComponentSingleSetter<Image, Sprite>
    {
        #region Methods

        protected override void OnValueChanged(Sprite newValue)
        {
            this.Target.enabled = newValue != null;
        }

        #endregion
    }
}