using Code.Gameplay.EffectApplication.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.EffectApplication
{
    public class EffectApplicationFeature: Feature
    {
        public EffectApplicationFeature(ISystemFactory systems)
        {
            Add(systems.Create<ApplyEffectsOnTargetsSystem>());
        }
    }
}