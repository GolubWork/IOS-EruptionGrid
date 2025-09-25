using Code.Gameplay.EffectsVisual.Configs;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.StaticData.VisualEffectStaticData
{
    public interface IVisualEffectStaticDataService
    {
        UniTask LoadAll();
        VisualEffectConfig GetVisualEffectConfig(VisualEffectTypeId effectTypeId);
    }
}