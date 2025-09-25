using Entitas;
using UnityEngine;

namespace Code.Gameplay.Backgrounds
{
    [Game] public class Background : IComponent { }
    [Game] public class CanvasComponent : IComponent { public Canvas Value; }
}