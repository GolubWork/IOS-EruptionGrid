using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Enviroment
{
    [Game] public class ObstacleLocations : IComponent { public HashSet<int> Value;}
    [Game] public class ObstacleLocationsInitialized : IComponent { }
    
}