using Code.Gameplay.Objects.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Objects
{
    public class ObjectPoolFeature: Feature
    {
        public ObjectPoolFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeObjectPoolSystem>());
            Add(systems.Create<DeactivateObjectPoolSystem>());
        }
    }
}