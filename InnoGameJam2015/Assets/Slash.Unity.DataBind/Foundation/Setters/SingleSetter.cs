// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using System.Diagnostics.CodeAnalysis;

    using Slash.Unity.DataBind.Core.Presentation;

    /// <summary>
    ///   Base class for a setter of a single data value.
    /// </summary>
    public abstract class SingleSetter : Setter
    {
        #region Fields

        /// <summary>
        ///   Data to bind to.
        /// </summary>
        public DataBinding Data;

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMemberHiearchy.Global")]
        protected virtual void Awake()
        {
            this.AddBinding(this.Data);
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMemberHiearchy.Global")]
        protected virtual void OnDestroy()
        {
            this.RemoveBinding(this.Data);
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual void OnDisable()
        {
            this.Data.ValueChanged -= this.OnObjectValueChanged;
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
        protected virtual void OnEnable()
        {
            this.Data.ValueChanged += this.OnObjectValueChanged;
            if (this.Data.IsInitialized)
            {
                this.OnObjectValueChanged(this.Data.Value);
            }
        }

        /// <summary>
        ///   Called when the data binding value changed.
        /// </summary>
        /// <param name="newValue">New data value.</param>
        protected virtual void OnObjectValueChanged(object newValue)
        {
        }

        #endregion
    }

    /// <summary>
    ///   Generic base class for a single data setter of a specific type.
    /// </summary>
    /// <typeparam name="T">Type of data to set.</typeparam>
    public abstract class SingleSetter<T> : SingleSetter
    {
        #region Methods

        protected override void OnObjectValueChanged(object newValue)
        {
            var value = newValue is T ? (T)newValue : this.Data.GetValue<T>();
            this.OnValueChanged(value);
        }

        /// <summary>
        ///   Called when the data binding value changed.
        /// </summary>
        /// <param name="newValue">New data value.</param>
        protected abstract void OnValueChanged(T newValue);

        #endregion
    }
}