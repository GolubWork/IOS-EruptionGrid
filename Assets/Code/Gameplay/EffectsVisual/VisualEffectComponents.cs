using Code.Gameplay.EffectsVisual.Configs;
using Entitas;

namespace Code.Gameplay.EffectsVisual
{
    [Game] public class VisualEffect : IComponent { }
    [Game] public class VisualEffectEffectValue : IComponent { public float Value; }
    [Game] public class VisualEffectProducerId : IComponent { public int Value; }
    [Game] public class VisualEffectTargetId : IComponent { public int Value; }
    
    [Game] public class VisualEffectConfigComponent : IComponent { public VisualEffectConfig Value; } 
    [Game] public class ProcessedVisualEffect : IComponent { } 
    
    [Game] public class TapVisualEffect : IComponent { }
    [Game] public class CollectEggVisualEffect : IComponent { }
}