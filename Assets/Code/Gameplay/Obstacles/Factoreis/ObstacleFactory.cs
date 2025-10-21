using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Obstacles.Configs;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Obstacles.Factoreis
{
    public class ObstacleFactory : IObstacleFactory
    {
        private readonly IIdentifierService _identifierService;

        public ObstacleFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateObstacle(ObstacleTypeId typeId, Vector3 at)
        {
            switch (typeId)
            {
                case ObstacleTypeId.Tree:
                {
                    return CreateTree(at);
                }
                case ObstacleTypeId.Currency:
                {
                    return CreateCurrency(at);
                }
                case ObstacleTypeId.Car:
                {
                    return CreateCar(at);
                }
            }
            return null;
        }

        private GameEntity CreateCurrency(Vector3 at)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddViewPath(PrefabsDirectoryConstants.CurrencyObstacle)
                    .With(e => e.isCurrencyObstacle = true)
                ;
        }

        public GameEntity CreateTree(Vector3 at)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddViewPath(PrefabsDirectoryConstants.TreeObstacle)
                    .With(e => e.isTreeObstacle = true)
                    ;
        }
        
        public GameEntity CreateCar(Vector3 at)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddViewPath(PrefabsDirectoryConstants.CarObstacle)
                    .With(e => e.isCarObstacle = true)
                    .With(e => e.isMovementAvailable = true)
                    .With(e => e.isMoving = true)
                    .With(e => e.isMoveWithNoBounds = true)
                    .With(e => e.isRotationAlignedAlongDirection = true)
                ;
        }
    }
}