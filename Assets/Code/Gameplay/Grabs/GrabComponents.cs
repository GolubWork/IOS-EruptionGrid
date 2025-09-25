using Entitas;

namespace Code.Gameplay.Grabs
{
    [Game] public class Grabable : IComponent { }
    [Game] public class Grabed : IComponent { }
    [Game] public class Placed : IComponent { }
    [Game] public class FollowMouseXY : IComponent { }
    [Game] public class FollowMouseY : IComponent { }
    [Game] public class FollowMouseX : IComponent { }
}