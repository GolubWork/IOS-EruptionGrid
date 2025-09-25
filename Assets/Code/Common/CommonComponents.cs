using Entitas;

namespace Code.Common
{
    [Game, Audio] public class Destructed : IComponent { }

    [Game] public class SelfDestructTimer : IComponent { public float Value; }
    [Game] public class PersistentComponent : IComponent { }

}