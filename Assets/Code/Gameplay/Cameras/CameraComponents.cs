using Entitas;
using UnityEngine;

namespace Code.Gameplay.Cameras
{
    [Game] public class CameraComponent : IComponent { public Camera Value; }
    [Game] public class BorderCamera : IComponent { }
    [Game] public class MainCamera : IComponent { }
    [Game] public class EdgeCollider2DComponent : IComponent { public EdgeCollider2D Value; }
    
}