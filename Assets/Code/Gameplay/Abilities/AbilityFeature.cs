using Code.Gameplay.Abilities.System;
using Code.Gameplay.Cooldowns.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Abilities
{
  public sealed class AbilityFeature : Feature
  {
    public AbilityFeature(ISystemFactory systems)
    {
      Add(systems.Create<CooldownSystem>());
      Add(systems.Create<DestroyAbilityEntitiesOnUpgradeSystem>());
      Add(systems.Create<DestroyAbilityEntitiesOnCleanUpSystem>());

      Add(systems.Create<CollisionEffectSystem>());
    }
  }
}