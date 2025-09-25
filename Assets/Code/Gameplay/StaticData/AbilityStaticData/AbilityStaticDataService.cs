using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Abilities;
using Code.Gameplay.Abilities.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Gameplay.StaticData.AbilityStaticData
{
    public class AbilityStaticDataService : IAbilityStaticDataService
    {
        private Dictionary<AbilityId,AbilityConfig> _abilityById;
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
            await LoadAbilities();
        }
        
        public AbilityConfig GetAbilityConfig(AbilityId abilityId)
        {
            if (_abilityById.TryGetValue(abilityId, out AbilityConfig config))
                return config;
            
            if (config.Levels == null || config.Levels.Count == 0)
            {
                throw new Exception($"No levels available for ability {abilityId}");
            }
            
            throw new Exception($"Ability config for {abilityId} was not found");
        }

        public AbilityLevel GetAbilityLevel(AbilityId abilityId, int level)
        {
            AbilityConfig config = GetAbilityConfig(abilityId);
            
            if (level > config.Levels.Count)
                level = config.Levels.Count;

            if (level < config.Levels.Count - config.Levels.Count)
                level = config.Levels.Count - config.Levels.Count;

            return config.Levels[level - 1];
        }
        
        private async UniTask LoadAbilities()
        {
            Dictionary<string, AbilityConfig> abilityConfigsByName = await _assetProvider.LoadAllScriptable<AbilityConfig>(DownloadServiceConstants.AbilitiesLabel);
            _abilityById = abilityConfigsByName.Values.ToDictionary(x => x.AbilityId, x => x);
        }
    }
}