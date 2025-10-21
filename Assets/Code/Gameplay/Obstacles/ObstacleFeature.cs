using Code.Gameplay.Obstacles.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Obstacles
{
    public class ObstacleFeature: Feature
    {
        public ObstacleFeature(ISystemFactory systems)
        {
            Add(systems.Create<CollisionWithCardObstacleSystem>());
            Add(systems.Create<CollisionWithCurrencyObstacle>());
        }
    }
}