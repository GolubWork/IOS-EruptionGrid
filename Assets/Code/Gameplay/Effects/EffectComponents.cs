using Code.Gameplay.Effects.Configs;
using Entitas;

namespace Code.Gameplay.Effects
{
    [Game] public class Effect : IComponent { }
    [Game] public class EffectValue : IComponent { public float Value; }
    [Game] public class ProducerId : IComponent { public int Value; }
    [Game] public class TargetId : IComponent { public int Value; }
    
    [Game] public class DamageEffect : IComponent { }
    [Game] public class HealEffect : IComponent { }
    [Game] public class AddPointsEffect : IComponent { }
    [Game] public class VerticalForceEffect : IComponent { }
    [Game] public class AddCurrencyEffect : IComponent { }
    [Game] public class TapEffect : IComponent { }
    
    [Game] public class DamageReflectionComponent : IComponent { public float Value; } 
    [Game] public class EffectSetupComponent : IComponent { public EffectSetup Value; } 
}