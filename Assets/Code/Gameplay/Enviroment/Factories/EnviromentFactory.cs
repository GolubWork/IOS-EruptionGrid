using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Enviroment.Configs;
using Code.Gameplay.Obstacles.Configs;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Enviroment.Factories
{
    public class EnviromentFactory : IEnviromentFactory
    {
        private readonly IIdentifierService _identifierService;

        public EnviromentFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateEnviroment(EnviromentTypeId typeId, Vector3 at)
        {
            switch (typeId)
            {
                case EnviromentTypeId.Grass:
                {
                    return CreateGrassEnviroment(at);
                }
                case EnviromentTypeId.Road:
                {
                    return CreateRoadEnviroment(at);
                }
            }

            return null;
        }

        private GameEntity CreateRoadEnviroment(Vector3 at)
        {
            return CreateGameEntity.Empty()
                .AddId(_identifierService.Next())
                .AddViewPath(PrefabsDirectoryConstants.RoadEnviroment)
                .AddWorldPosition(at)
                .AddObstacleLocations(new HashSet<int>())
                .AddObstacleTypeId(ObstacleTypeId.Car);
        }

        public GameEntity CreateGrassEnviroment(Vector3 at)
        {
            ObstacleTypeId obstacleTypeId = EnumExtensions<ObstacleTypeId>.GetRandom();
            while (obstacleTypeId == ObstacleTypeId.Car)
            {
                obstacleTypeId = EnumExtensions<ObstacleTypeId>.GetRandom();  
            }
            return CreateGameEntity.Empty()
                .AddId(_identifierService.Next())
                .AddViewPath(PrefabsDirectoryConstants.GrassEnviroment)
                .AddWorldPosition(at)
                .AddObstacleLocations(new HashSet<int>())
                .AddObstacleTypeId(obstacleTypeId);
        }

        public GameEntity CreateHomeEnviroment(Vector3 at)
        {
            return CreateGameEntity.Empty()
                .AddId(_identifierService.Next())
                .AddViewPath(PrefabsDirectoryConstants.GrassEnviroment)
                .AddWorldPosition(at);
        }
    }
}