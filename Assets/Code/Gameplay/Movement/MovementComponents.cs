using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement
{
    [Game] public class Speed : IComponent { public float Value; }
    [Game] public class Direction : IComponent { public Vector2 Value; }
    [Game] public class Moving : IComponent { }
    [Game] public class MovementAvailable : IComponent { }
    [Game] public class MovableByInput : IComponent { }
    [Game] public class TurnedAlongDirection : IComponent { }
    [Game] public class RotationAlignedAlongDirection : IComponent { }
    [Game] public class FullRotationAlignedAlongDirection : IComponent { }
    [Game] public class RotationRandomDirection : IComponent { }
    [Game] public class MoveInCameraBounds : IComponent { }
    [Game] public class MoveWithNoBounds : IComponent { }
    [Game] public class RotationSpeed : IComponent { public float Value; }
    

}