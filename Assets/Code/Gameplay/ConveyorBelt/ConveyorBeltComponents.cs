using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.ConveyorBelt
{
    [Game] public class ConveyorBelt: IComponent { }   
    [Game] public class ConveyorBeltSpeed: IComponent { public float Value; }
    [Game] public class LineRendererComponent : IComponent { public LineRenderer Value; }
    [Game] public class ConveyourItemsIds : IComponent { public List<int> Value; }
    [Game] public class LinkedBeltId : IComponent { public int Value; }
}