// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConditionalFormatter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Formatters
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Returns one of two values, depending on a specified condition.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Formatters/[DB] Conditional Formatter")]
    public class ConditionalFormatter : DataProvider
    {
        #region Fields

        public DataBinding Condition;

        public DataBinding FalseValue;

        public DataBinding TrueValue;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var condition = this.Condition.GetValue<bool>();
                return condition ? this.TrueValue.Value : this.FalseValue.Value;
            }
        }

        #endregion

        #region Methods

        protected void Awake()
        {
            // Add bindings.
            this.AddBinding(this.Condition);
            this.AddBinding(this.TrueValue);
            this.AddBinding(this.FalseValue);
        }

        protected override void UpdateValue()
        {
            // TODO(co): Cache current value and check if really changed?
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}