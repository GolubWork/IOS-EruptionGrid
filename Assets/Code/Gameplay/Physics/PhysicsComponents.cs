using Entitas;
using UnityEngine;

namespace Code.Gameplay.Physics
{
    [Game] public class PhysicsBody : IComponent { }
    [Game] public class Velocity : IComponent { public Vector2 Value; }
    [Game] public class Rigidbody2DComponent : IComponent { public Rigidbody2D Value; }
    [Game] public class BoxCollider2DComponent : IComponent { public BoxCollider2D Value; }
    [Game] public class BoxColliderComponent : IComponent { public BoxCollider Value; }
    [Game] public class ForceApplier : IComponent { }
    [Game] public class ForceTargetId : IComponent { public int Value; }
    [Game] public class ForceProducerId : IComponent { public int Value; }
    [Game] public class PhysicsForce : IComponent { public Vector2 Value; }
    [Game] public class Processed : IComponent { }
    
}