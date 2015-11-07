﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringFormatter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Providers.Formatters
{
    using System;
    using System.Linq;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    /// <summary>
    ///   Formats arguments by a specified format string to create a new string value.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Formatters/[DB] String Formatter")]
    public class StringFormatter : DataProvider
    {
        #region Fields

        /// <summary>
        ///   Arguments to put into the string.
        /// </summary>
        public DataBinding[] Arguments;

        /// <summary>
        ///   Format to use.
        /// </summary>
        public DataBinding Format;

        #endregion

        #region Properties

        public override object Value
        {
            get
            {
                var format = string.Empty;
                if (this.Format != null)
                {
                    format = this.Format.GetValue<string>();
                }

                var texts = this.Arguments.Select(argument => argument.Value).ToArray();
                string newValue = null;
                try
                {
                    newValue = !string.IsNullOrEmpty(format)
                        ? texts.Length > 0 ? string.Format(format, texts) : format
                        : string.Empty;
                }
                catch (Exception e)
                {
                    Debug.LogError("Exception formatting value: " + e);
                }

                return newValue;
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
            this.AddBinding(this.Format);
            this.AddBindings(this.Arguments);
        }

        protected override void UpdateValue()
        {
            // TODO(co): Cache current value and check if really changed?
            this.OnValueChanged(this.Value);
        }

        #endregion
    }
}