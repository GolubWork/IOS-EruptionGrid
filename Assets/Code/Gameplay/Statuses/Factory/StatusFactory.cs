using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Enchants;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Statuses.Factory
{
  public class StatusFactory : IStatusFactory
  {
    private readonly IIdentifierService _identifiers;

    public StatusFactory(IIdentifierService identifiers)
    {
      _identifiers = identifiers;
    }

    public GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId)
    {
      GameEntity status;
      switch (setup.StatusTypeId)
      {
        case StatusTypeId.ExplosiveEnchant:
          status = CreateExplosiveEnchantStatus(setup, producerId, targetId);     
          break;
        case StatusTypeId.Explosive:
          status = CreateExplosiveStatus(setup, producerId, targetId);
          break;
        
        default:
          throw new Exception($"Status with type id {setup.StatusTypeId} does not exist");
      }

      status
        .With(x => x.AddDuration(setup.Duration), when: setup.Duration > 0)
        .With(x => x.AddTimeLeft(setup.Duration), when: setup.Duration > 0)
        .With(x => x.AddPeriod(setup.Period), when: setup.Period > 0)
        .With(x => x.AddTimeSinceLastTick(0), when: setup.Period > 0)
        ;
      
      return status;
    }
    
    private GameEntity CreateExplosiveEnchantStatus(StatusSetup setup, int producerId, int targetId)
    {
      return CreateGameEntity.Empty()
          .AddId(_identifiers.Next())
          .AddStatusTypeId(StatusTypeId.ExplosiveEnchant)
          .AddEnchantTypeId(EnchantTypeId.ExplosiveArmaments)
          .AddEffectValue(setup.Value)
          .AddProducerId(producerId)
          .AddTargetId(targetId)
          .With(x => x.isStatus = true)
          .With(x => x.isExplosiveEnchant = true)
        ;
    }
    
    private GameEntity CreateExplosiveStatus(StatusSetup setup, int producerId, int targetId)
    {
      return CreateGameEntity.Empty()
          .AddId(_identifiers.Next())
          .AddStatusTypeId(StatusTypeId.ExplosiveEnchant)
          .AddEffectValue(setup.Value)
          .AddProducerId(producerId)
          .AddTargetId(targetId)
          .With(x => x.isStatus = true)
          .With(x => x.isExplosive = true)
        ;
    }
  }
}