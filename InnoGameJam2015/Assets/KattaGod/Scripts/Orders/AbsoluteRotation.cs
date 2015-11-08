namespace KattaGod.Orders
{
    using UnityEngine;

    public class AbsoluteRotation : MonoBehaviour
    {
        #region Fields

        public Vector3 RotationEuler;

        #endregion

        #region Methods

        protected void Start()
        {
            this.UpdateRotation();
        }

        [ContextMenu("Update Rotation")]
        private void UpdateRotation()
        {
            this.transform.rotation = Quaternion.Euler(this.RotationEuler);
        }

        #endregion
    }
}