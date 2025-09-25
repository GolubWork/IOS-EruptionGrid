using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Code.Gameplay.Common
{
    [Game, Audio, Meta] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
    [Game] public class EntityLink : IComponent { [EntityIndex] public int Value; }
    [Game, Audio] public class WorldPosition : IComponent { public Vector3 Value; }
    [Game] public class Damage : IComponent { public float Value; }
    [Game] public class Active : IComponent { }
    [Game] public class InitializedComponent : IComponent { }
    [Game] public class Inactive : IComponent { }
}
