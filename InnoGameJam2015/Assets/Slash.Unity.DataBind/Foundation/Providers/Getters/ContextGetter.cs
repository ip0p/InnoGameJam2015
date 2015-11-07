// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextGetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Getters
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Provides the current context as a data value.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Getters/[DB] Context Getter")]
    public class ContextGetter : DataProvider
    {
        #region Properties

        public override object Value
        {
            get
            {
                var contextHolder = this.GetComponentInParent<ContextHolder>();
                return contextHolder != null ? contextHolder.Context : null;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void OnContextChanged()
        {
            base.OnContextChanged();

            this.UpdateValue();
        }

        #endregion

        #region Methods

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}