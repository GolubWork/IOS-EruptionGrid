using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Configs;
using Entitas;

namespace Code.Gameplay.Collection
{
    [Game] public class Collectable : IComponent { }
    [Game] public class Collected : IComponent { }
    [Game] public class CollectEffect : IComponent { public EffectConfig Value; }
}