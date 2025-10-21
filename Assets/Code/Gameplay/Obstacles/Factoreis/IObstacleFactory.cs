using Code.Gameplay.Obstacles.Configs;
using UnityEngine;

namespace Code.Gameplay.Obstacles.Factoreis
{
    public interface IObstacleFactory
    {
        GameEntity CreateObstacle(ObstacleTypeId typeId, Vector3 at);
        GameEntity CreateTree(Vector3 at);
    }
}