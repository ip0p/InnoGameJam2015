namespace KattaGod.World.Contexts
{
    using KattaGod.Inventory.Contexts;

    using Slash.Unity.DataBind.Core.Data;

    using UnityEngine;

    public class WorldContext : Context
    {
        public void DoDropItem(ItemContext item)
        {
            Debug.Log("Dropped item " + item);
        }
    }
}