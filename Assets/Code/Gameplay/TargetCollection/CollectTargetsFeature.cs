using Code.Gameplay.TargetCollection.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.TargetCollection
{
    public class CollectTargetsFeature: Feature
    {
        public CollectTargetsFeature(ISystemFactory systems)
        {
            Add(systems.Create<CollectTargetsIntervalSystem>());
            Add(systems.Create<CastForTargetsNoLimitSystem>());
            Add(systems.Create<CastForTargetsWithLimitSystem>());
            Add(systems.Create<MarkReachedSystem>());
            Add(systems.Create<CleanupTargetBuffersSystem>());
        }
    }
}