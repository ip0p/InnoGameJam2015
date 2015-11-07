// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataProvider.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Core.Presentation
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using UnityEngine;

    /// <summary>
    ///   Base class for a data provider.
    /// </summary>
    public abstract class DataProvider : MonoBehaviour
    {
        #region Fields

        /// <summary>
        ///   Child bindings.
        /// </summary>
        private readonly List<DataBinding> bindings = new List<DataBinding>();

        #endregion

        #region Delegates

        /// <summary>
        ///   Delegate for ValueChanged event.
        /// </summary>
        /// <param name="newValue">New data value.</param>
        public delegate void ValueChangedDelegate(object newValue);

        #endregion

        #region Events

        /// <summary>
        ///   Triggered when the value changed.
        /// </summary>
        public event ValueChangedDelegate ValueChanged;

        #endregion

        #region Properties

        /// <summary>
        ///   Indicates if the data provider already holds a valid value.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.bindings == null || this.bindings.All(binding => binding.IsInitialized);
            }
        }

        /// <summary>
        ///   Current data value.
        /// </summary>
        public abstract object Value { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Has to be called when an anchestor context changed as the data value may change.
        /// </summary>
        public virtual void OnContextChanged()
        {
            foreach (var binding in this.bindings)
            {
                binding.OnContextChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Adds and initializes the specified binding.
        /// </summary>
        /// <param name="binding">Binding to add.</param>
        protected void AddBinding(DataBinding binding)
        {
            // Init.
            binding.Init(this.gameObject);

            binding.ValueChanged += this.OnBindingValueChanged;

            this.bindings.Add(binding);
        }

        /// <summary>
        ///   Adds and initializes the specified bindings.
        /// </summary>
        /// <param name="newBindings">Bindings to add.</param>
        protected void AddBindings(IEnumerable<DataBinding> newBindings)
        {
            foreach (var binding in newBindings)
            {
                this.AddBinding(binding);
            }
        }

        /// <summary>
        ///   Should be called by a derived class if the value of the data provider changed.
        /// </summary>
        /// <param name="newValue">New value of this data provider.</param>
        protected void OnValueChanged(object newValue)
        {
            var handler = this.ValueChanged;
            if (handler != null)
            {
                handler(newValue);
            }
        }

        /// <summary>
        ///   Removes and deinitializes the specified binding.
        /// </summary>
        /// <param name="binding">Binding to remove.</param>
        protected void RemoveBinding(DataBinding binding)
        {
            binding.ValueChanged -= this.OnBindingValueChanged;

            // Deinit.
            binding.Deinit();

            this.bindings.Remove(binding);
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMemberHiearchy.Global")]
        protected virtual void Start()
        {
            this.UpdateValue();
        }

        /// <summary>
        ///   Called when the value of the data provider should be updated.
        /// </summary>
        protected abstract void UpdateValue();

        private void OnBindingValueChanged(object newValue)
        {
            this.UpdateValue();
        }

        #endregion
    }
}