using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.Cooldowns;
using Code.Gameplay.StaticData.AbilityStaticData;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Abilities.Factory
{
  public class AbilityFactory : IAbilityFactory
  {
    private readonly IIdentifierService _identifiers;
    private readonly IAbilityStaticDataService _staticDataService;

    public AbilityFactory(IIdentifierService identifiers, IAbilityStaticDataService staticDataService)
    {
      _identifiers = identifiers;
      _staticDataService = staticDataService;
    }

    public GameEntity MeteorAbility(int producerId, int level)
    {
      AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.Meteor, level);
      return CreateGameEntity.Empty()
        .AddId(_identifiers.Next())
        .AddAbilityId(AbilityId.Meteor)
        .AddProducerId(producerId)
        .AddCooldown(abilityLevel.Cooldown)
        .With(x => x.isVegetableBoltAbility = true)
        .PutOnCooldown();
    }
  }
}