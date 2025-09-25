using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Enchants;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.StaticData.EnchantStaticData
{
    public class EnchantStaticDataService : IEnchantStaticDataService
    {
        private Dictionary<EnchantTypeId, EnchantConfig> _enchantById;
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
            await LoadEnchants();
        }
        
        public EnchantConfig GetEnchantConfig(EnchantTypeId typeId)
        {
            if (_enchantById.TryGetValue(typeId, out EnchantConfig config))
                return config;

            throw new Exception($"Enchant config for {typeId} was not found");
        }
        
        private async UniTask LoadEnchants()
        {
            Dictionary<string, EnchantConfig> abilityConfigsByName = await _assetProvider.LoadAllScriptable<EnchantConfig>(DownloadServiceConstants.EnchantsLabel);
            _enchantById = abilityConfigsByName.Values.ToDictionary(x => x.TypeId, x => x);
        }
    }
}