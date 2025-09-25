using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.EffectStaticData
{
    public interface IEffectStaticDataService
    {
        UniTask LoadAll();
        EffectConfig GetEffectConfig(EffectTypeId effectTypeId);
    }
}