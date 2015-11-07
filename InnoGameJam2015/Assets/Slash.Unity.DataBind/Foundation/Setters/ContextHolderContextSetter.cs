// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextHolderContextSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Sets the context of a specific context holder.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Context Holder Context Setter")]
    public class ContextHolderContextSetter : ComponentSingleSetter<ContextHolder, object>
    {
        #region Methods

        protected override void OnValueChanged(object newValue)
        {
            this.Target.Context = newValue;
        }

        #endregion
    }
}