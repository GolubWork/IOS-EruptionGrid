using Entitas;
using UnityEngine;

namespace Code.Input
{
    [Input] public class Input : IComponent {}
    [Input] public class TouchInput : IComponent { }
    [Input] public class AxisInput : IComponent { public Vector2 Value; }
    [Input] public class TouchPosition : IComponent { public Vector2 Value; }
    [Input] public class TouchPhaseComponent : IComponent { public TouchPhase Value; }
    [Input] public class InputAvaliable : IComponent {  }

}