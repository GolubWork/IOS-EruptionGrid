using Entitas;
using UnityEngine;

namespace Code.Gameplay.Common
{
    [Game] public class TransformComponent : IComponent { public Transform Value; }
    [Game] public class SpriteRendererComponent : IComponent { public SpriteRenderer Value; }
    [Game] public class MeshRendererComponent : IComponent { public MeshRenderer Value; }

}