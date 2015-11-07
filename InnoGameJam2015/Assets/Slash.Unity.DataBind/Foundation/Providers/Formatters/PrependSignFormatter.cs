// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrependSignFormatter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Formatters
{
    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Prepends in front of the data value.
    ///   <para>Input: Number</para>
    ///   <para>Output: String</para>
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Formatters/[DB] Prepend Sign")]
    public class PrependSignFormatter : DataProvider
    {
        #region Fields

        /// <summary>
        ///   Data value to use.
        /// </summary>
        public DataBinding Data;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var argumentA = this.Data.GetValue<float>();
                return string.Format("{0}{1}", argumentA > 0 ? "+" : string.Empty, argumentA);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void Awake()
        {
            // Add bindings.
            this.AddBinding(this.Data);
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}