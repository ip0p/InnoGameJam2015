﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageFillAmountSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.UI.Unity.Setters
{
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    ///   Set the fill amount of an Image depending on the data value.
    /// </summary>
    [AddComponentMenu("Data Bind/UnityUI/Setters/[DB] Image Fill Amount Setter (Unity)")]
    public class ImageFillAmountSetter : ComponentSingleSetter<Image, float>
    {
        #region Methods

        protected override void OnValueChanged(float newValue)
        {
            this.Target.fillAmount = newValue;
        }

        #endregion
    }
}