namespace KattaGod.Orders
{
    using UnityEngine;

    public class AlertExpireBehaviour : MonoBehaviour
    {
        #region Fields

        public float Acceleration = 1;

        public float InitialSpeed = 1;

        public Vector3 Move = new Vector3(1, 0, 0);

        public float Speed = 1;

        public GameObject Target;

        private float t;

        #endregion

        #region Public Methods and Operators

        public void Update()
        {
            this.Speed += this.Acceleration * Time.deltaTime;
            this.t += this.Speed * Time.deltaTime;
            this.Target.transform.localPosition = Vector3.Lerp(-this.Move, this.Move, Mathf.PingPong(this.t, 1));
        }

        #endregion

        #region Methods

        protected void OnEnable()
        {
            this.t = 0;
            this.Speed = this.InitialSpeed;
        }

        #endregion
    }
}