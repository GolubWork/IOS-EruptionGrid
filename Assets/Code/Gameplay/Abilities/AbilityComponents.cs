using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Abilities
{
  [Game] public class AbilityIdComponent : IComponent { public AbilityId Value; }
  [Game] public class ParentAbility : IComponent { [EntityIndex] public AbilityId Value; }
  [Game] public class VegetableBoltAbility : IComponent { }
  [Game] public class CollisionEffectComponent : IComponent { public GameEntityBehaviour Value; }
  [Game] public class OrbitingMushroomAbility : IComponent { }
  [Game] public class GarlicAuraAbility : IComponent { }
  [Game] public class UpgradeRequest : IComponent { }
  [Game] public class RecreatedOnUpgrade : IComponent { }
}