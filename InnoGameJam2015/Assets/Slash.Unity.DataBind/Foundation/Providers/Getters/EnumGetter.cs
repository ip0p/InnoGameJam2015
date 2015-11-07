// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumGetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Getters
{
    using System;

    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;

    using UnityEngine;

    public class EnumGetter : DataProvider
    {
        #region Fields

        [SerializeField]
        [TypeSelection(BaseType = typeof(Enum))]
        private string enumType;

        #endregion

        #region Properties

        /// <summary>
        ///   Type of enum to get.
        /// </summary>
        public Type EnumType
        {
            get
            {
                try
                {
                    return this.enumType != null ? ReflectionUtils.FindType(this.enumType) : null;
                }
                catch (TypeLoadException)
                {
                    Debug.LogError("Can't find type '" + this.enumType + "'.", this);
                    return null;
                }
            }
            set
            {
                this.enumType = value != null ? value.AssemblyQualifiedName : null;
            }
        }

        public override object Value
        {
            get
            {
                return Enum.GetValues(this.EnumType);
            }
        }

        #endregion

        #region Methods

        protected override void UpdateValue()
        {
        }

        #endregion
    }
}