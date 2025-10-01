// UpdatableWindowFactory.cs
using Code.Gameplay.StaticData.WindowsStaticData;
using Code.Infrastructure.DependencyInjection;
using UnityEngine;

namespace Code.Windows.UpdatableWindows
{
    public class UpdatableWindowFactory : IUpdatableWindowFactory
    {
        private readonly IWindowsStaticDataService _windowsStaticData;
        private readonly IInstantiator _instantiator;

        private RectTransform _uiRoot; // инстанс корня сцены

        public UpdatableWindowFactory(IWindowsStaticDataService windowsStaticData, IInstantiator instantiator)
        {
            _windowsStaticData = windowsStaticData;
            _instantiator = instantiator;
        }

        public void SetUiRoot(GameObject uiRoot) => _uiRoot = uiRoot.gameObject.GetComponent<RectTransform>();

        public UpdatableWindow CreateWindow(UpdatableWindowId id)
        {
            if (_uiRoot == null)
                throw new System.InvalidOperationException("[UpdatableWindowFactory] uiRoot is null. Call SetUiRoot(...) first.");

            var prefab = _windowsStaticData.GetUpdatableWindowPrefab(id);
            if (prefab == null)
                throw new System.InvalidOperationException($"[UpdatableWindowFactory] Prefab for {id} is null.");

            // диагностика: убеждаемся, что префаб содержит UpdatableWindow-компонент
            if (!prefab.TryGetComponent<UpdatableWindow>(out var _))
                Debug.LogError($"[UpdatableWindowFactory] Prefab for {id} не содержит компонент UpdatableWindow. Проверь WindowsStaticData.");

            return _instantiator.InstantiatePrefabForComponent<UpdatableWindow>(prefab, _uiRoot, false);
        }
        
    }
}