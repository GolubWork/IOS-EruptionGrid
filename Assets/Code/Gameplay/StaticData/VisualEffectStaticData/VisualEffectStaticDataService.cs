using System;
using System.Collections.Generic;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.StaticData.VisualEffectStaticData
{
    public class VisualEffectStaticDataService : IVisualEffectStaticDataService
    {
        private Dictionary<VisualEffectTypeId, VisualEffectConfig> _effectById = new Dictionary<VisualEffectTypeId, VisualEffectConfig>();
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
            await LoadEffects();
        }
        
        public VisualEffectConfig GetVisualEffectConfig(VisualEffectTypeId effectTypeId)
        {
            if (_effectById.TryGetValue(effectTypeId, out VisualEffectConfig config))
                return config;
            
            throw new Exception($"VisualEffect config for {effectTypeId} was not found");
        }
        
        
        private async UniTask LoadEffects()
        {
            VisualEffectConfigs visualEffectConfigs = 
                await _assetProvider.LoadScriptable<VisualEffectConfigs>(ConfigsDirectoryConstants.VisualEffects);
            foreach (VisualEffectConfig config in visualEffectConfigs.configs)
            {
                _effectById.TryAdd(config.effectTypeId, config);
            }
        }
    }
}