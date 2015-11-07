namespace KattaGod.Initialization
{
    using System.Collections;

    using KattaGod.Hud.Contexts;
    using KattaGod.World.Contexts;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    public class Initializer : MonoBehaviour
    {
        #region Fields

        public string HudScene = "Hud";

        public string WorldScene = "World";

        #endregion

        #region Public Methods and Operators

        public void Start()
        {
            // Load world.
            this.AddScene(this.WorldScene, new WorldContext());

            // Load hud.
            this.AddScene(this.HudScene, new DummyHudContext());
        }

        #endregion

        #region Methods

        private void AddScene(string sceneName, object context)
        {
            this.StartCoroutine(this.AddSceneAsync(sceneName, context));
        }

        private IEnumerator AddSceneAsync(string sceneName, object context)
        {
            yield return Application.LoadLevelAdditiveAsync(sceneName);

            yield return new WaitForEndOfFrame();

            var rootGameObject = GameObject.Find(sceneName);
            if (rootGameObject != null)
            {
                var contextHolder = rootGameObject.GetComponent<ContextHolder>();
                if (contextHolder != null)
                {
                    contextHolder.Context = context;
                }
            }
        }

        #endregion
    }
}