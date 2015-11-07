// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Context.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Core.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Slash.Unity.DataBind.Core.Utils;

    /// <summary>
    ///   Base class for a data context which contains properties to bind to.
    /// </summary>
    public abstract class Context
    {
        #region Constants

        public const char PathSeparator = '.';

        #endregion

        #region Fields

        /// <summary>
        ///   Root data node.
        /// </summary>
        private readonly DataNode root;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructor.
        /// </summary>
        protected Context()
        {
            this.root = new DataNode(this);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Registers a callback at the specified path of the context.
        /// </summary>
        /// <param name="path">Path to register for.</param>
        /// <param name="onValueChanged">Callback to invoke when value at the specified path changed.</param>
        /// <exception cref="ArgumentException">Thrown if path is invalid for this context.</exception>
        /// <returns>Current value at specified path.</returns>
        public object RegisterListener(string path, Action<object> onValueChanged)
        {
            var node = this.root.FindDescendant(path);
            if (node == null)
            {
                throw new ArgumentException("Invalid path '" + path + "' for type " + this.GetType(), "path");
            }

            // Register for value change.
            node.ValueChanged += onValueChanged;

            return node.Value;
        }

        /// <summary>
        ///   Removes the callback from the specified path of the context.
        /// </summary>
        /// <param name="path">Path to remove callback from.</param>
        /// <param name="onValueChanged">Callback to remove.</param>
        /// <exception cref="ArgumentException">Thrown if path is invalid for this context.</exception>
        public void RemoveListener(string path, Action<object> onValueChanged)
        {
            var node = this.root.FindDescendant(path);
            if (node == null)
            {
                throw new ArgumentException("Invalid path '" + path + "' for type " + this.GetType(), "path");
            }

            // Remove from value change.
            node.ValueChanged -= onValueChanged;
        }

        /// <summary>
        ///   Sets the specified value at the specified path.
        /// </summary>
        /// <param name="path">Path to set the data value at.</param>
        /// <exception cref="ArgumentException">Thrown if path is invalid for this context.</exception>
        /// <exception cref="InvalidOperationException">Thrown if data at specified path can't be changed.</exception>
        /// <param name="value">Value to set.</param>
        public void SetValue(string path, object value)
        {
            var node = this.root.FindDescendant(path);
            if (node == null)
            {
                throw new ArgumentException("Invalid path '" + path + "' for type " + this.GetType(), "path");
            }

            node.SetValue(value);
        }

        #endregion

        /// <summary>
        ///   Wraps a data object in the context tree and makes sure that the registered listeners are informed
        ///   when the data value changed.
        /// </summary>
        private sealed class DataNode
        {
            #region Fields

            private object parentObject;

            /// <summary>
            ///   Data property to get informed if value changes.
            /// </summary>
            private Property property;

            private object value;

            #endregion

            #region Constructors and Destructors

            private DataNode(NodeTypeInfo typeInfo)
            {
                this.Children = new List<DataNode>();
                this.TypeInfo = typeInfo;
            }

            public DataNode(Context context)
            {
                this.Children = new List<DataNode>();
                this.TypeInfo = new NodeTypeInfo { Type = context.GetType() };
                this.Value = context;
            }

            #endregion

            #region Events

            public event Action<object> ValueChanged;

            #endregion

            #region Properties

            public object Value
            {
                get
                {
                    return this.value;
                }
                private set
                {
                    if (value == this.value)
                    {
                        return;
                    }

                    this.value = value;

                    this.OnValueChanged(this.value);

                    // Update children parent object.
                    foreach (var childNode in this.Children)
                    {
                        childNode.ParentObject = this.value;
                    }
                }
            }

            private List<DataNode> Children { get; set; }

            private string Name { get; set; }

            private object ParentObject
            {
                get
                {
                    return this.parentObject;
                }
                set
                {
                    if (value == this.parentObject)
                    {
                        return;
                    }

                    this.parentObject = value;

                    // Update data property and value.
                    this.UpdateContent();
                }
            }

            private Property Property
            {
                get
                {
                    return this.property;
                }
                set
                {
                    if (value == this.property)
                    {
                        return;
                    }

                    if (this.property != null)
                    {
                        this.property.ValueChanged -= this.OnPropertyValueChanged;
                    }

                    this.property = value;

                    if (this.property != null)
                    {
                        this.property.ValueChanged += this.OnPropertyValueChanged;
                    }
                }
            }

            /// <summary>
            ///   Cached type information of the data value this node capsules.
            /// </summary>
            private NodeTypeInfo TypeInfo { get; set; }

            #endregion

            #region Public Methods and Operators

            public DataNode FindDescendant(string path)
            {
                var pointPos = path.IndexOf(PathSeparator);
                var nodeName = path;
                string pathRest = null;
                if (pointPos >= 0)
                {
                    nodeName = path.Substring(0, pointPos);
                    pathRest = path.Substring(pointPos + 1);
                }

                // Get children with name.
                var childNode = this.GetChild(nodeName);
                if (childNode == null)
                {
                    return null;
                }

                return String.IsNullOrEmpty(pathRest) ? childNode : childNode.FindDescendant(pathRest);
            }

            public void SetValue(object newValue)
            {
                // Update data value.
                this.TypeInfo.SetValue(this.ParentObject, newValue);

                // Update cached value.
                this.Value = newValue;
            }

            #endregion

            #region Methods

            private DataNode CreateChild(string name)
            {
                // Get type of child.
                var typeInfo = this.GetChildTypeInfo(name);
                if (typeInfo == null)
                {
                    // No child with this name.
                    return null;
                }

                var childNode = new DataNode(typeInfo) { Name = name, ParentObject = this.value };
                childNode.UpdateContent();
                this.Children.Add(childNode);
                return childNode;
            }

            private DataNode GetChild(string name)
            {
                var childNode = this.Children.FirstOrDefault(child => child.Name == name) ?? this.CreateChild(name);
                return childNode;
            }

            private NodeTypeInfo GetChildTypeInfo(string name)
            {
                // Get item if collection.
                if (this.TypeInfo.Type.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    // Check if index provided.
                    int itemIndex;
                    if (Int32.TryParse(name, out itemIndex))
                    {
                        // Return item.
                        return new EnumerableNode() { Type = this.TypeInfo.Type.GetElementType(), Index = itemIndex };
                    }
                }

                // Get property.
                var reflectionProperty = ReflectionUtils.GetPublicProperty(this.TypeInfo.Type, name);
                if (reflectionProperty != null)
                {
                    return new PropertyNode { Type = reflectionProperty.PropertyType, Property = reflectionProperty };
                }

                // Get field.
                var reflectionField = ReflectionUtils.GetPublicField(this.TypeInfo.Type, name);
                if (reflectionField != null)
                {
                    return new FieldNode { Type = reflectionField.FieldType, Field = reflectionField };
                }

                // Get method.
                var reflectionMethod = ReflectionUtils.GetPublicMethod(this.TypeInfo.Type, name);
                if (reflectionMethod != null)
                {
                    return new MethodNode { Type = reflectionMethod.ReturnType, Method = reflectionMethod };
                }

                return null;
            }            
            
            /// <summary>
            ///   Returns the data property holder of the specified object and type with the specified name.
            /// </summary>
            /// <param name="obj">Concrete object to get data property holder for.</param>
            /// <param name="name">Name of data property to get holder for.</param>
            /// <returns>Data property holder for the specified object and name.</returns>
            private static Property GetProperty(object obj, string name)
            {
                if (obj == null || String.IsNullOrEmpty(name))
                {
                    return null;
                }

                // Go up the type hierarchy of the object.
                // NOTE(co): Private fields of base classes are not reflected for derived types.
                var type = obj.GetType();
                while (type != null)
                {
                    var property = GetProperty(type, obj, name);
                    if (property != null)
                    {
                        return property;
                    }
                    type = type.BaseType;
                }

                return null;
            }

            /// <summary>
            ///   Returns the data property holder of the specified object and type with the specified name.
            /// </summary>
            /// <param name="type">Type to search.</param>
            /// <param name="obj">Concrete object to get data property for.</param>
            /// <param name="name">Name of data property to get holder for.</param>
            /// <returns>Data property holder for the specified object and name, using the specified type for reflection.</returns>
            private static Property GetProperty(Type type, object obj, string name)
            {
                // Check for lower case field.
                var lowerCaseName = Char.ToLowerInvariant(name[0]) + name.Substring(1) + "Property";
                var propertyField = ReflectionUtils.GetPrivateField(type, lowerCaseName);
                if (propertyField != null)
                {
                    return propertyField.GetValue(obj) as Property;
                }

                // Check for public property.
                var propertyProperty = ReflectionUtils.GetPublicProperty(type, name + "Property");
                if (propertyProperty != null)
                {
                    return propertyProperty.GetValue(obj, null) as Property;
                }

                // Check for field.
                propertyField = ReflectionUtils.GetPrivateField(type, name + "Property");
                if (propertyField != null)
                {
                    return propertyField.GetValue(obj) as Property;
                }

                return null;
            }

            private void OnPropertyValueChanged()
            {
                // Update cached value.
                this.Value = this.Property.Value;
            }

            private void OnValueChanged(object obj)
            {
                var handler = this.ValueChanged;
                if (handler != null)
                {
                    handler(obj);
                }
            }

            private void UpdateContent()
            {
                // Get property of the node.
                this.Property = GetProperty(this.ParentObject, this.Name);

                // Get object of the node.
                this.Value = this.TypeInfo.GetValue(this.ParentObject);
            }

            #endregion

            private class FieldNode : NodeTypeInfo
            {
                #region Properties

                public FieldInfo Field { private get; set; }

                #endregion

                #region Public Methods and Operators

                public override object GetValue(object obj)
                {
                    if (obj == null)
                    {
                        return null;
                    }

                    // Get field value.
                    if (this.Field != null)
                    {
                        return this.Field.GetValue(obj);
                    }

                    return null;
                }

                public override void SetValue(object obj, object value)
                {
                    if (obj == null)
                    {
                        return;
                    }

                    if (this.Field == null)
                    {
                        return;
                    }

                    // Set field value.
                    this.Field.SetValue(obj, value);
                }

                #endregion
            }

            private class PropertyNode : NodeTypeInfo
            {
                #region Properties

                public PropertyInfo Property { private get; set; }

                #endregion

                #region Public Methods and Operators

                public override object GetValue(object obj)
                {
                    if (obj == null)
                    {
                        return null;
                    }

                    // Get property value.
                    if (this.Property != null)
                    {
                        return this.Property.GetValue(obj, null);
                    }

                    return null;
                }

                public override void SetValue(object obj, object value)
                {
                    if (obj == null)
                    {
                        return;
                    }

                    if (this.Property == null)
                    {
                        return;
                    }

                    // Set property value.
                    if (this.Property.CanWrite)
                    {
                        this.Property.SetValue(obj, value, null);
                    }
                    else
                    {
                        throw new InvalidOperationException("Property '" + this.Property.Name + "' is read-only.");
                    }
                }

                #endregion
            }

            private class MethodNode : NodeTypeInfo
            {
                #region Properties

                public MethodInfo Method { private get; set; }

                #endregion

                #region Public Methods and Operators

                public override object GetValue(object obj)
                {
                    if (obj == null)
                    {
                        return null;
                    }

                    // Get delegate.
                    if (this.Method != null)
                    {
                        var args = new List<Type>(this.Method.GetParameters().Select(p => p.ParameterType));
                        var delegateType = Expression.GetActionType(args.ToArray());
                        return ReflectionUtils.CreateDelegate(delegateType, obj, this.Method);
                    }

                    return null;
                }

                #endregion
            }

            private class NodeTypeInfo
            {
                #region Properties

                public Type Type { get; set; }

                #endregion

                #region Public Methods and Operators

                /// <summary>
                ///   Returns the value using the type info on the specified object.
                /// </summary>
                /// <param name="obj">Object to use the type info on.</param>
                /// <returns>Current value when using type info on specified object.</returns>
                public virtual object GetValue(object obj)
                {
                    throw new NotImplementedException();
                }

                public virtual void SetValue(object obj, object value)
                {
                    throw new InvalidOperationException("Data node of type '" + this.Type + "' is read-only.");
                }

                #endregion
            }

            private class EnumerableNode : NodeTypeInfo
            {
                #region Properties

                public int Index { get; set; }

                #endregion

                #region Public Methods and Operators

                public override object GetValue(object obj)
                {
                    // Check if enumerable.
                    var enumerable = obj as IEnumerable;
                    if (enumerable == null)
                    {
                        return null;
                    }

                    var index = 0;
                    foreach (var item in enumerable)
                    {
                        if (index == this.Index)
                        {
                            return item;
                        }
                        ++index;
                    }

                    return null;
                }

                #endregion
            }
        }
    }
}