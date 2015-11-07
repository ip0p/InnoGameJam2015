// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextHolderEditor.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    ///   Custom editor for <see cref="ContextHolder"/>.
    /// </summary>
    [CustomEditor(typeof(ContextHolder))]
    public class ContextHolderEditor : Editor
    {
        #region Fields

        private readonly string[] contextTypeNames;

        private readonly List<Type> contextTypes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructor.
        /// </summary>
        public ContextHolderEditor()
        {
            this.contextTypes = new List<Type> { null };
            var availableContextTypes =
                ReflectionUtils.FindTypesWithBase<Context>().Where(type => !type.IsAbstract).ToList();
            availableContextTypes.Sort(
                (typeA, typeB) => String.Compare(typeA.FullName, typeB.FullName, StringComparison.Ordinal));
            this.contextTypes.AddRange(availableContextTypes);
            this.contextTypeNames = this.contextTypes.Select(type => type != null ? type.FullName : "None").ToArray();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Unity callback.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var contextHolder = this.target as ContextHolder;
            if (contextHolder == null)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();

            if (!Application.isPlaying)
            {
                // Find all available context classes.
                var contextTypeIndex = this.contextTypes.IndexOf(contextHolder.ContextType);
                var newContextTypeIndex = EditorGUILayout.Popup("Context Type", contextTypeIndex, this.contextTypeNames);
                if (newContextTypeIndex != contextTypeIndex)
                {
                    contextHolder.ContextType = this.contextTypes[newContextTypeIndex];
                }

                // Should a context of the specified type be created at startup?
                contextHolder.CreateContext =
                    EditorGUILayout.Toggle(
                        new GUIContent("Create context?", "Create context on startup?"),
                        contextHolder.CreateContext);
            }
            else
            {
                var contextType = contextHolder.Context != null ? contextHolder.Context.GetType().ToString() : "null";
                EditorGUILayout.LabelField("Context", contextType);
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(contextHolder);
            }
        }

        #endregion
    }
}