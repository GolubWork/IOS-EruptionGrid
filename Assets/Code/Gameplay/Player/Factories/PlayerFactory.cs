using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Player.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IIdentifierService _identifierService;

        public PlayerFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreatePlayer(Vector3 at)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                .AddWorldPosition(at)
                
                .AddViewPath(PrefabsDirectoryConstants.PlayerPrefabPath)
                
                .AddMaxHP(1)
                .AddCurrentHP(1)
                
                .AddOneStepMovementBoundsX(new Vector2Int(-5, 5))
                .AddOneStepMovementBoundsY(new Vector2Int(-10, 0))
                
                .With(e => e.isRequireSkinApplication = true) 
                .With(e => e.isOneStepMovement = true)
                .With(e => e.isSwipeMovement = true)
                .With(e => e.isPlayer = true)
                
                    .With(e => e.isRequireSkinApplication = true)
                 ;
        }
    }
}