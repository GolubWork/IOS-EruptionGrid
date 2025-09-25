using Code.Gameplay.Effects.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Effects
{
    public class EffectFeature: Feature
    {
        public EffectFeature(ISystemFactory systems)
        {
            Add(systems.Create<RemoveEffectsWithoutTargetsSystem>());
            Add(systems.Create<ProcessAddScorePointsEffectSystem>());
            Add(systems.Create<ProcessTapEffectSystem>());
            Add(systems.Create<ProcessAddCurrencyEffect>());
            Add(systems.Create<CleanUpProcessedEffects>());
        }
    }
}