using Code.Gameplay.StaticData.WindowsStaticData;
using UnityEngine;
using Zenject;

namespace Code.Windows.UpdatableWindows
{
    public class UpdatableWindowFactory: IUpdatableWindowFactory
    {
        private readonly IWindowsStaticDataService _windowsStaticData;
        private readonly IInstantiator _instantiator;
        
        private GameObject _uiRootPrefab;
        
        public UpdatableWindowFactory(IWindowsStaticDataService windowsStaticData, IInstantiator instantiator)
        {
            _windowsStaticData = windowsStaticData;
            _instantiator = instantiator;
        }

        public void SetUiRoot(GameObject uiRoot) => _uiRootPrefab = uiRoot;

        public UpdatableWindow CreateWindow(UpdatableWindowId updatableWindowId)
        {
            GameObject uiRoot = _instantiator.InstantiatePrefab(_uiRootPrefab);
            return _instantiator.InstantiatePrefabForComponent<UpdatableWindow>(PrefabFor(updatableWindowId), uiRoot.transform);
        }
        
        private GameObject PrefabFor(UpdatableWindowId id) => _windowsStaticData.GetUpdatableWindowPrefab(id);
    }
}