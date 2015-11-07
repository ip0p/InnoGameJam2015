namespace Slash.Unity.DataBind.Foundation.Setters
{
    using System.Linq;

    using UnityEngine;

    /// <summary>
    ///   Adds items under specified fixed slots.
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Setters/[DB] Slot Items Setter")]
    public class SlotItemsSetter : GameObjectItemsSetter<MonoBehaviour>
    {
        #region Fields

        /// <summary>
        ///   Slots to add items to.
        /// </summary>
        public Transform[] Slots;

        #endregion

        #region Methods

        protected override Transform GetParent(object itemContext)
        {
            // Get empty slot.
            var slotTransform = this.GetEmptySlot();
            if (slotTransform == null)
            {
                Debug.LogWarning("No empty slot remaining, won't create object for item", this);
                return null;
            }

            return slotTransform;
        }

        private Transform GetEmptySlot()
        {
            var itemGameObjects = this.ItemGameObjects.ToList();

            // Get first slot where no item was placed under.
            return
                this.Slots.FirstOrDefault(
                    slot => itemGameObjects.All(itemGameObject => itemGameObject.transform.parent != slot));
        }

        #endregion
    }
}