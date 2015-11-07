// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObjectItemsSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Foundation.Setters
{
    using System.Collections.Generic;
    using System.Linq;

    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;

    using UnityEngine;

    /// <summary>
    ///   Base class which adds game objects for each item of an ItemsSetter.
    /// </summary>
    /// <typeparam name="TBehaviour"></typeparam>
    public abstract class GameObjectItemsSetter<TBehaviour> : ItemsSetter<TBehaviour>
        where TBehaviour : MonoBehaviour
    {
        #region Fields

        /// <summary>
        ///   Items.
        /// </summary>
        private readonly List<Item> items = new List<Item>();

        /// <summary>
        ///   Prefab to create the items from.
        /// </summary>
        public GameObject Prefab;

        #endregion

        #region Properties

        protected IEnumerable<GameObject> ItemGameObjects
        {
            get
            {
                return this.items.Select(item => item.GameObject);
            }
        }

        #endregion

        #region Methods

        protected override sealed void ClearItems()
        {
            foreach (var item in this.items)
            {
                Destroy(item.GameObject);
            }
            this.items.Clear();
        }

        protected override sealed void CreateItem(object itemContext)
        {
            var parent = this.GetParent(itemContext);
            if (parent == null)
            {
                Debug.LogWarning("No parent provided for item, won't create item object.", this);
                return;
            }

            var item = this.InstantiateItem(parent, itemContext);
            this.items.Add(new Item { GameObject = item, Context = itemContext });
            this.OnItemCreated(itemContext, item);
        }

        protected virtual Transform GetParent(object itemContext)
        {
            return this.Target.transform;
        }

        protected virtual void OnItemCreated(object itemContext, GameObject itemObject)
        {
        }

        protected override sealed void RemoveItem(object itemContext)
        {
            // Get item.
            var item = this.items.FirstOrDefault(existingItem => existingItem.Context == itemContext);
            if (item == null)
            {
                Debug.LogWarning("No item found for collection item " + itemContext, this);
                return;
            }

            // Destroy item.
            Destroy(item.GameObject);

            // Remove item from collection.
            this.items.Remove(item);
        }

        private GameObject InstantiateItem(Transform parent, object itemContext)
        {
            var item = parent.gameObject.AddChild(this.Prefab);
            if (itemContext != null)
            {
                // Set item data context.
                var itemContextHolder = item.GetComponent<ContextHolder>();
                if (itemContextHolder == null)
                {
                    itemContextHolder = item.AddComponent<ContextHolder>();
                }
                itemContextHolder.Context = itemContext;
            }
            return item;
        }

        #endregion

        private class Item
        {
            #region Properties

            public object Context { get; set; }

            public GameObject GameObject { get; set; }

            #endregion
        }
    }
}