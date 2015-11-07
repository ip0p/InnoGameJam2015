// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupTextUtils.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace UnityApplication.View.Texts.Editor
{
    using UnityEditor;

    using UnityEngine;

    public class SetupTextUtils
    {
        #region Public Methods and Operators

        [MenuItem("CONTEXT/Text/Set Style: Big")]
        public static void ApplyStyleBig()
        {
            ApplyStyle(Selection.activeGameObject, "Big");
        }

        [MenuItem("CONTEXT/Text/Set Style: Medium")]
        public static void ApplyStyleMedium()
        {
            ApplyStyle(Selection.activeGameObject, "Medium");
        }

        [MenuItem("CONTEXT/Text/Set Style: Small")]
        public static void ApplyStyleSmall()
        {
            ApplyStyle(Selection.activeGameObject, "Small");
        }

        [MenuItem("KattaGod/UI/Text Setup/Settings")]
        public static void EditTextSettings()
        {
            Selection.activeObject = TextSettings.Instance;
        }

        [MenuItem("KattaGod/UI/Text Setup/Re-Apply styles")]
        public static void ReapplyStyles()
        {
            var textSetups = Object.FindObjectsOfType<TextSetup>();
            foreach (var textSetup in textSetups)
            {
                textSetup.Apply();
            }
        }

        #endregion

        #region Methods

        private static void ApplyStyle(GameObject gameObject, string style)
        {
            var textSetup = gameObject.GetComponent<TextSetup>() ?? gameObject.AddComponent<TextSetup>();
            textSetup.Style = style;
            textSetup.Apply();
        }

        #endregion
    }
}