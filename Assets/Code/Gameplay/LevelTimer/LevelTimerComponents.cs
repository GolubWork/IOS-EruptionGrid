using System;
using Entitas;

namespace Code.Gameplay.LevelTimer
{
    [Game] public class LevelTimer : IComponent { }
    [Game] public class TimerCompleted : IComponent { }
    [Game] public class CurrentTimer : IComponent { public float Value; }
    [Game] public class MaxTimer : IComponent { public float Value; }
}