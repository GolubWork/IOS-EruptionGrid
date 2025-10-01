using UnityEngine;

namespace Code.Infrastructure.DependencyInjection
{
    public static class ProjectContextAutoBootstrap
    {
        // Выполняется до загрузки первой сцены
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void EnsureDiRoot()
        {
            if (DiContext.Instance != null) return;

            var prefab = Resources.Load<GameObject>("DI/ProjectContextRoot");
            if (prefab != null)
            {
                var go = Object.Instantiate(prefab);
                Object.DontDestroyOnLoad(go); // ВАЖНО: переживаем смену сцены
            }
        }
    }
}