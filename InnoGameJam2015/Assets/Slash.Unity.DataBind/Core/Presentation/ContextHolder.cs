// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextHolder.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Core.Presentation
{
    using System;

    using Slash.Unity.DataBind.Core.Utils;
    using Slash.Unity.DataBind.Foundation.Commands;
    using Slash.Unity.DataBind.Foundation.Setters;

    using UnityEngine;

    /// <summary>
    ///   Holds a data context to specify the context to use on the presentation side.
    /// </summary>
    [AddComponentMenu("Data Bind/Core/[DB] Context Holder")]
    public class ContextHolder : MonoBehaviour
    {
        #region Fields

        private object context;

        [SerializeField]
        [HideInInspector]
        private string contextType;

        [SerializeField]
        [HideInInspector]
        private bool createContext;

        #endregion

        #region Properties

        /// <summary>
        ///   Data context.
        /// </summary>
        public object Context
        {
            get
            {
                return this.context;
            }
            set
            {
                if (value == this.context)
                {
                    return;
                }

                this.context = value;

                this.OnContextChanged();
            }
        }

        /// <summary>
        ///   Type of context to create on startup.
        /// </summary>
        public Type ContextType
        {
            get
            {
                try
                {
                    return this.contextType != null ? ReflectionUtils.FindType(this.contextType) : null;
                }
                catch (TypeLoadException)
                {
                    Debug.LogError("Can't find context type '" + this.contextType + "'.", this);
                    return null;
                }
            }
            set
            {
                this.contextType = value != null ? value.AssemblyQualifiedName : null;
            }
        }

        /// <summary>
        ///   Indicates if a context should be created from the specified context type.
        /// </summary>
        public bool CreateContext
        {
            get
            {
                return this.createContext;
            }
            set
            {
                this.createContext = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected virtual void Awake()
        {
            if (this.Context == null && this.ContextType != null && this.CreateContext)
            {
                this.Context = Activator.CreateInstance(this.ContextType);
            }
        }

        /// <summary>
        ///   Called when the context of this holder changed.
        /// </summary>
        protected virtual void OnContextChanged()
        {
            // Update child bindings as context changed.
            var uiBindings = this.gameObject.GetComponentsInChildren<Setter>(true);
            foreach (var binding in uiBindings)
            {
                binding.OnContextChanged();
            }
            var providers = this.gameObject.GetComponentsInChildren<DataProvider>(true);
            foreach (var provider in providers)
            {
                provider.OnContextChanged();
            }
            var commands = this.gameObject.GetComponentsInChildren<Command>(true);
            foreach (var command in commands)
            {
                command.OnContextChanged();
            }
        }

        #endregion
    }
}