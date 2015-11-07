// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualityCheck.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Checks
{
    using System;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Checks for equality of two data values.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Checks/[DB] Equality Check")]
    public class EqualityCheck : DataProvider
    {
        #region Fields

        /// <summary>
        ///   First data value.
        /// </summary>
        public DataBinding First;

        /// <summary>
        ///   Second data value.
        /// </summary>
        public DataBinding Second;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var firstValue = this.First.Value;
                var secondValue = this.Second.Value;
                return CheckValues(firstValue, secondValue);
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
            this.AddBinding(this.First);
            this.AddBinding(this.Second);
        }

        protected override void UpdateValue()
        {
            this.OnValueChanged(this.Value);
        }

        private static bool CheckValues(object firstValue, object secondValue)
        {
            if (firstValue == secondValue)
            {
                return true;
            }

            if (firstValue == null || secondValue == null)
            {
                return false;
            }

            // Change type of second value to compare.
            var firstValueType = firstValue.GetType();
            try
            {
                var secondValueConverted = Convert.ChangeType(secondValue, firstValueType);
                return Equals(firstValue, secondValueConverted);
            }
            catch (InvalidCastException)
            {
                // Try cast enum.
                if (firstValueType.IsEnum && secondValue is string)
                {
                    try
                    {
                        var secondValueConverted = Enum.Parse(firstValueType, (string)secondValue);
                        return Equals(firstValue, secondValueConverted);
                    }
                    catch (ArgumentException)
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        #endregion
    }
}