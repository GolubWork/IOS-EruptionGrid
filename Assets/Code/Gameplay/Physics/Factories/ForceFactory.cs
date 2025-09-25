using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Physics.Factories
{
    public class ForceFactory : IForceFactory
    {
        private readonly IIdentifierService _identifierService;

        public ForceFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateForce(int producerId, int targetId, Vector2 power)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddPhysicsForce(power)
                    .AddForceProducerId(producerId)
                    .AddForceTargetId(targetId)
                    .With(e => e.isForceApplier = true)
                ;
        }
    }
}