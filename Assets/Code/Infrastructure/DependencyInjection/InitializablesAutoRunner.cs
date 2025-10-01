using UnityEngine;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Простой автораннер: после инъекции сцен (Awake/sceneLoaded у тебя уже настроены),
    /// вызовет Initialize()/LateInitialize() на всём дереве под этим GO.
    /// Повесь на тот же объект, где у тебя GameInstaller/DiContext.
    /// </summary>
    public class InitializablesAutoRunner : MonoBehaviour
    {
        [Tooltip("Включая неактивные объекты.")]
        public bool includeInactive = true;

        [Tooltip("Запустить автоматически в Start().")]
        public bool runOnStart = true;

        private void Start()
        {
            if (!runOnStart) return;
            InitializableUtility.RunOnGameObject(gameObject, includeChildren: includeInactive);
        }

        /// <summary>Можно дернуть вручную из кода после своей кастомной инъекции.</summary>
        public void RunNowOnThisTree()
        {
            InitializableUtility.RunOnGameObject(gameObject, includeChildren: includeInactive);
        }
    }
}