using Code.Gameplay.Enviroment.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Enviroment
{
    public class EnviromentFeature: Feature
    {
        public EnviromentFeature(ISystemFactory systems)
        {
            Add(systems.Create<CreateEnviromentSystem>());
            Add(systems.Create<InitializeObstacleLocationsSystem>());
            Add(systems.Create<CreateNewEnviromentOnPlayerMoveSystem>());
        }
    }
}