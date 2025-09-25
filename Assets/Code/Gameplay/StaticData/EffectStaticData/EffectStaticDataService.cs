using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.StaticData.EffectStaticData
{
    public class EffectStaticDataService : IEffectStaticDataService
    {
        private Dictionary<EffectTypeId, EffectConfig> _effectById;
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
        
        public EffectConfig GetEffectConfig(EffectTypeId effectTypeId)
        {
            if (_effectById.TryGetValue(effectTypeId, out EffectConfig config))
                return config;
            
            throw new Exception($"Effect config for {effectTypeId} was not found");
        }
        
        
        private async UniTask LoadEffects()
        {
            Dictionary<string, EffectConfig> abilityConfigsByName = 
                await _assetProvider.LoadAllScriptable<EffectConfig>(DownloadServiceConstants.EffectsLabel);
            
            _effectById = abilityConfigsByName.Values.ToDictionary(x => x.EffectTypeId, x => x);
        }
    }
}