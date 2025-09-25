using Code.Gameplay.Armaments.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Armaments
{
    public sealed class ArmamentFeature: Feature
    {
        public ArmamentFeature(ISystemFactory systems)
        {
            Add(systems.Create<MarkProcessedOnTargetLimitExceededSystem>());
            Add(systems.Create<FollowProducerSystem>());
            Add(systems.Create<FinalizeProcessedArmamentsSystem>());
        }
    }
}