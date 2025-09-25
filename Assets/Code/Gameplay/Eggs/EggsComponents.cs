using Entitas;

namespace Code.Gameplay.Eggs
{
    [Game] public class Egg : IComponent { }
    [Game] public class EggLifeTime : IComponent { public float Value; }
    [Game] public class EggCurrentSpawnTime : IComponent { public float Value; }
    [Game] public class EggMaxSpawnTime : IComponent { public float Value; }
    [Game] public class EggSpawnActive : IComponent { }
}