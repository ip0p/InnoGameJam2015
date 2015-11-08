namespace KattaGod.Orders
{
    using UnityEngine;
    using UnityEngine.UI;

    public class AlertExpireColorizeBehaviour : MonoBehaviour
    {
        #region Fields

        public SpriteRenderer Target;

        public Color TargetColor = Color.red;

        public float Duration = 3.0f;

        private float t;

        #endregion

        protected void OnEnable()
        {
            this.t = 0;
        }

        #region Public Methods and Operators

        public void Update()
        {
            this.t += Time.deltaTime / this.Duration;
            this.Target.color = Color.Lerp(Color.white, this.TargetColor, this.t);
        }

        #endregion
    }
}