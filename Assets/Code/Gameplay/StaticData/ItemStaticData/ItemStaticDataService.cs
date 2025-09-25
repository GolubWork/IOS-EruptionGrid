using System.Collections.Generic;
using Code.Gameplay.Items.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.StaticData.ItemStaticData
{
    public class ItemStaticDataService : IItemStaticDataService
    {
        private ItemConfigs _config;
        private Dictionary<ProducingItemTypeId, ItemConfig> configs = new Dictionary<ProducingItemTypeId, ItemConfig>();
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
        
        public ItemConfigs GetConfig() => _config;

        public ItemConfig GetConfigById(ProducingItemTypeId typeId)
        {
            configs.TryGetValue(typeId, out ItemConfig config);
            return config;
        }
        
        private async UniTask LoadConfigs()
        {
            _config = 
                await _assetProvider.LoadScriptable<ItemConfigs>(ConfigsDirectoryConstants.ItemConfig);

            foreach (ItemConfig config in _config.configs)
            {
                configs[config.typeId] = config;
            }
        }
    }
}