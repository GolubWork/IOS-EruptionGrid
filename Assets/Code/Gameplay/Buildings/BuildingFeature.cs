using Code.Gameplay.Buildings.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Buildings
{
    public class BuildingFeature: Feature
    {
        public BuildingFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeBuildingSystem>());
        }
    }
}