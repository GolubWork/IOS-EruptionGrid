using Code.Gameplay.Bucket.Systems;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Bucket
{
    public class BucketFeature: Feature
    {
        public BucketFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeBucketSystem>());
            Add(systems.Create<CollectEggOnAABBSystem>());
        }
    }
}