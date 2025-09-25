using Code.Gameplay.Effects.Configs;
using Code.Gameplay.EffectsVisual.Configs;
using Entitas;

namespace Code.Gameplay.Taps
{
    [Game] public class Tapable : IComponent { }
    [Game] public class Taped : IComponent { }

    [Game] public class TapsRequired : IComponent { public int Value; }
    [Game] public class TotalTapsRequired : IComponent { public int Value; }
    [Game] public class TapDepleted : IComponent { }
    
    [Game] public class TapEffectConfig : IComponent { public EffectConfig Value; }
    [Game] public class TapVisualEffectConfigComponent : IComponent { public VisualEffectConfig Value; }
    
    
    [Game] public class TapRapeatableTimes : IComponent { public int Value; } // -1 is infinity tapRepeat
}