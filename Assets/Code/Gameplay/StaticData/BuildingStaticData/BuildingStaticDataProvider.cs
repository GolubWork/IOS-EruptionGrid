using System.Collections.Generic;
using Code.Gameplay.Buildings.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.StaticData.BuildingStaticData
{
    public class BuildingStaticDataProvider : IBuildingStaticDataProvider
    {
        private BuildingConfigs _config;
        private Dictionary<BuildingTypeId, BuildingConfig> configs = new Dictionary<BuildingTypeId, BuildingConfig>();
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
            await LoadConfigs();
        }
        
        public BuildingConfigs GetConfig() => _config;

        public BuildingConfig GetConfigById(BuildingTypeId typeId)
        {
            configs.TryGetValue(typeId, out BuildingConfig config);
            return config;
        }
        
        private async UniTask LoadConfigs()
        {
            _config = 
                await _assetProvider.LoadScriptable<BuildingConfigs>(ConfigsDirectoryConstants.BuildingConfig);

            foreach (BuildingConfig config in _config.Buildings)
            {
                configs[config.TypeId] = config;
            }
        }
    }
}