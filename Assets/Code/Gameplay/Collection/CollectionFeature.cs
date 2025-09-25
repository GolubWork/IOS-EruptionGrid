using Code.Gameplay.Collection.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Collection
{
    public class CollectionFeature: Feature
    {
        public CollectionFeature(ISystemFactory systems)
        {
            Add(systems.Create<CreateEffectOnCollectSystem>());
            Add(systems.Create<MarkDestructCollectedSystem>());
        }
    }
}