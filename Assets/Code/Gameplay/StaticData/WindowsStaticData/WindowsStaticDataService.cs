using System;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Windows.StaticWindows;
using Code.Windows.StaticWindows.Configs;
using Code.Windows.UpdatableWindows;
using Code.Windows.UpdatableWindows.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.StaticData.WindowsStaticData
{
    public class WindowsStaticDataService: IWindowsStaticDataService
    {
        private Dictionary<StaticWindowId, GameObject> _staticWindowPrefabsById;
        private Dictionary<UpdatableWindowId, GameObject> _updatableWindowPrefabsById;
        
        private GameObject _canvasPrefab;
        private IAssetProvider _assetProvider;


        [Inject]
        private void Construct(
            IAssetProvider assetProvider
        )
        {
            _assetProvider = assetProvider;
        }
        
        public async UniTask LoadAll()
        {
           await LoadWindowsAsync();
        }
        
        public GameObject GetCanvasPrefab() => _canvasPrefab;
        public GameObject GetStaticWindowPrefab(StaticWindowId id) =>
            _staticWindowPrefabsById.TryGetValue(id, out GameObject prefab)
                ? prefab
                : throw new Exception($"Prefab config for static window {id} was not found");        
        
        public GameObject GetUpdatableWindowPrefab(UpdatableWindowId id) =>
            _updatableWindowPrefabsById.TryGetValue(id, out GameObject prefab)
                ? prefab
                : throw new Exception($"Prefab config for updatable window {id} was not found");
        
        private async UniTask LoadWindowsAsync()
        {
            StaticWindowsConfig staticWindowsConfig = 
                await _assetProvider.LoadScriptable<StaticWindowsConfig>(ConfigsDirectoryConstants.StaticWindowsPath);
            
            UpdatableWindowsConfig updatableWindowsConfig = 
                await _assetProvider.LoadScriptable<UpdatableWindowsConfig>(ConfigsDirectoryConstants.UpdatableWindowsPath);

            _canvasPrefab = updatableWindowsConfig.canvasPrefab;

            _staticWindowPrefabsById = staticWindowsConfig
                .WindowConfigs
                .ToDictionary(x => x.Id, x => x.Prefab);

            _updatableWindowPrefabsById = updatableWindowsConfig
                .windowConfigs
                .ToDictionary(x => x.Id, x => x.Prefab);
        }
    }
}