using Entitas;

namespace Code.ConveyourBeltItem
{
    [Game] public class ConveyourBeltItem : IComponent {  }
    [Game] public class CurrentLerp : IComponent { public float Value; }
    [Game] public class StartPoint : IComponent { public int Value; }
    [Game] public class ItemSpacing : IComponent { public float Value; }
    [Game] public class Distance : IComponent { public float Value; }
    [Game] public class EndPoint : IComponent { public int Value; }
    [Game] public class ReachedEnd: IComponent { }
}