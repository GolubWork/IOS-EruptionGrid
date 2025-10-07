using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.DependencyInjection
{
    [DefaultExecutionOrder(-31990)]
    public sealed class SceneInjector : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;

            TryInjectAll("OnEnable");
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void Awake()
        {
            TryInjectAll("Awake");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InjectScene(scene, "sceneLoaded");
        }

        private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            if (newScene.IsValid()) InjectScene(newScene, "activeSceneChanged");
        }

        private void InjectScene(Scene scene, string reason)
        {
            var ctx = DiContext.Instance;
            if (ctx == null || ctx.Container == null) return;

            var roots = scene.GetRootGameObjects();
            for (int i = 0; i < roots.Length; i++)
                ctx.InjectGameObject(roots[i], includeChildren: true);

            try { InitializableUtility.RunOnGameObject(scene.GetRootGameObjects()[0].scene.GetRootGameObjects()[0], true); } catch { /* опционально */ }

        }

        public void TryInjectAll(string reason)
        {
            var ctx = DiContext.Instance;
            if (ctx == null || ctx.Container == null) return;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (!scene.IsValid() || !scene.isLoaded) continue;

                var roots = scene.GetRootGameObjects();
                for (int r = 0; r < roots.Length; r++)
                    ctx.InjectGameObject(roots[r], includeChildren: true);
            }

        }
    }
}
