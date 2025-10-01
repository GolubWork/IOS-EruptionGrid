using Code.Infrastructure.DependencyInjection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
    /// <summary>
    /// В редакторе: если контейнер ещё не поднят (DiContext.Instance == null),
    /// то при запуске любой сцены переключаемся на Entry-сцену (по имени или индексу).
    /// Выполняется максимально рано.
    /// </summary>
    [DefaultExecutionOrder(-32000)]
    public class SwitchToEntrySceneInEditor : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool useSceneName = false;
        [SerializeField] private string entrySceneName = "Entry"; // если useSceneName = true
        [SerializeField] private int entrySceneIndex = 0;         // иначе используем индекс

        private static bool _switchTriggered;

        private void Awake()
        {
            // если уже переключались — выходим
            if (_switchTriggered) return;

            // если контейнер уже есть, ничего не делаем
            if (DiContext.Instance != null) return;

            var active = SceneManager.GetActiveScene();

            // если уже в entry-сцене — выходим
            if (useSceneName ? active.name == entrySceneName
                    : active.buildIndex == entrySceneIndex) return;

            _switchTriggered = true;

            // выключаем руты, чтобы они не продолжали исполняться
            var roots = active.GetRootGameObjects();
            for (int i = 0; i < roots.Length; i++)
                roots[i].SetActive(false);

            // грузим Entry
            if (useSceneName)
                SceneManager.LoadScene(entrySceneName, LoadSceneMode.Single);
            else
                SceneManager.LoadScene(entrySceneIndex, LoadSceneMode.Single);
        }

#if UNITY_EDITOR
        // Если включены Enter Play Mode Options без domain reload —
        // сбросим флаг при старте Play Mode.
        [UnityEditor.InitializeOnEnterPlayMode]
        private static void ResetFlag(UnityEditor.EnterPlayModeOptions _)
        {
            _switchTriggered = false;
        }
#endif
#endif // UNITY_EDITOR
    }
}