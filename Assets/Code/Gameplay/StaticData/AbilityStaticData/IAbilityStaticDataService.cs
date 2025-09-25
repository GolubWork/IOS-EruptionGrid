using Code.Gameplay.Abilities;
using Code.Gameplay.Abilities.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.AbilityStaticData
{
    public interface IAbilityStaticDataService
    {
        UniTask LoadAll();
        AbilityConfig GetAbilityConfig(AbilityId abilityId);
        AbilityLevel GetAbilityLevel(AbilityId abilityId, int level);
    }
}