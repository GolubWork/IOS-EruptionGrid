using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Effects.Configs;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Effects.Factory
{
    public class EffectFactory : IEffectFactory
    {
        private readonly IIdentifierService _identifierService;

        public EffectFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateEffect(EffectSetup setup, int producerId, int targetId)
        {
            switch (setup.EffectTypeId)
            {
                case EffectTypeId.Unknown:
                {
                    break;
                }
                case EffectTypeId.AddPoints:
                {
                    return CreateAddPoints(producerId, targetId, setup.Value);
                }
                case EffectTypeId.AddCurrency:
                {
                    return CreateAddCurrency(producerId, targetId, setup.Value);
                }
                case EffectTypeId.Damage:
                {
                    return CreateDamage(producerId, targetId, setup.Value);
                }
                case EffectTypeId.Tap:
                {
                    return CreateTap(producerId, targetId, setup.Value);
                }
            }

            throw new Exception($"Effect with type id {setup.EffectTypeId} does not exist");
        }

        private GameEntity CreateTap(int producerId, int targetId, float setupValue)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .With(x => x.isEffect = true)
                    .With(x => x.isTapEffect = true)
                    .AddProducerId(producerId)
                    .AddTargetId(targetId)
                    .AddEffectValue(setupValue)
                ;
        }

        public GameEntity CreateEffect(EffectConfig setup, int producerId, int targetId)
         => CreateEffect(CreateSetup(setup), producerId, targetId);
        

        private EffectSetup CreateSetup(EffectConfig config)
        {
            return new EffectSetup()
            {
                EffectTypeId = config.EffectTypeId,
                Value = config.Value,
            };
        }

        private GameEntity CreateAddCurrency(int producerId, int targetId, float value)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .With(x => x.isEffect = true)
                    .With(x => x.isAddCurrencyEffect = true)
                    .AddProducerId(producerId)
                    .AddTargetId(targetId)
                    .AddEffectValue(value)
                ;
        }
        
        private GameEntity CreateAddPoints(int porudcerID, int targetId, float value)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .With(e => e.isEffect = true)
                    .With(e => e.isAddPointsEffect = true)
                    .AddProducerId(porudcerID)
                    .AddTargetId(targetId)
                    .AddEffectValue(value)
                ;
        }
        private GameEntity CreateDamage(int porudcerID, int targetId, float value)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .With(e => e.isEffect = true)
                    .With(e => e.isDamageEffect = true)
                    .AddProducerId(porudcerID)
                    .AddTargetId(targetId)
                    .AddEffectValue(value)
                ;
        }
    }
}