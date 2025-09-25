using Code.Gameplay.Eggs.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Eggs
{
    public class EggFeature: Feature
    {
        public EggFeature(ISystemFactory systems)
        {
            Add(systems.Create<ReturnEggOnLifeTimeSystem>());
            Add(systems.Create<SpawnEggOnTimerSystem>());
        }
    }
}