namespace UnityApplication.View.Texts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class TextEffectSettings
    {
        #region Fields

        public Color Color;

        public Vector2 Distance;

        public bool UseEffect;

        #endregion
    }

    [Serializable]
    public class TextStyleSettings
    {
        #region Fields

        public string Name;

        public int Size = 50;

        #endregion
    }

    public class TextSettings : GameSettings
    {
        #region Fields

        public Font Font;

        public TextEffectSettings OutlineSettings;

        public TextEffectSettings ShadowSettings;

        public List<TextStyleSettings> Styles;

        #endregion

        #region Properties

        public static TextSettings Instance
        {
            get
            {
                return LoadSettings<TextSettings>(
                    BuildResourceFolder("KattaGod"),
                    BuildResourceFilename<TextSettings>());
            }
        }

        #endregion

        #region Public Methods and Operators

        public void ApplyStyle(GameObject gameObject, string styleName)
        {
            var style = this.Styles.FirstOrDefault(existingStyle => existingStyle.Name == styleName);
            if (style == null)
            {
                Debug.LogWarningFormat("Text style '{0}' not found", styleName);
                return;
            }
            this.SetupText(gameObject, style);
#if UNITY_EDITOR
            EditorUtility.SetDirty(gameObject);
#endif
        }

        #endregion

        #region Methods

        private static void SetupEffect<TEffect>(GameObject gameObject, TextEffectSettings effectSettings)
            where TEffect : Shadow
        {
            if (effectSettings.UseEffect)
            {
                var effect = gameObject.GetComponent<TEffect>() ?? gameObject.AddComponent<TEffect>();
                effect.effectColor = effectSettings.Color;
                effect.effectDistance = effectSettings.Distance;
            }
        }

        private void SetupText(GameObject gameObject, TextStyleSettings style)
        {
            var text = gameObject != null ? gameObject.GetComponent<Text>() : null;
            if (text == null)
            {
                return;
            }

            text.font = this.Font;
            text.fontSize = style.Size;

            // Use shadow if required.
            SetupEffect<Shadow>(text.gameObject, this.ShadowSettings);

            // Use outline if required.
            SetupEffect<Outline>(text.gameObject, this.OutlineSettings);
        }

        #endregion
    }
}