using Code.Gameplay.Obstacles.Configs;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Obstacles
{
    [Game] public class TreeObstacle : IComponent { }
    [Game] public class CurrencyObstacle : IComponent { }
    [Game] public class CarObstacle : IComponent { }
    [Game] public class ObstacleTypeIdComponent : IComponent { public ObstacleTypeId Value; }
    [Game] public class InitialeWorldPosition : IComponent { public Vector3  Value; }
    
}