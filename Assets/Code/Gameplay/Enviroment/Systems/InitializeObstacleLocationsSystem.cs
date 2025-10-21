using System.Collections.Generic;
using Code.Gameplay.Obstacles.Configs;
using Code.Gameplay.Obstacles.Factoreis;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Enviroment.Systems
{
    public class InitializeObstacleLocationsSystem: IExecuteSystem
    {
        private readonly IObstacleFactory _obstacleFactory;
        private readonly IGroup<GameEntity> _obstacleLocations;
        private List<GameEntity> _buffer = new (6);

        public InitializeObstacleLocationsSystem(GameContext game, IObstacleFactory obstacleFactory)
        {
            _obstacleFactory = obstacleFactory;
            _obstacleLocations = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.ObstacleLocations,
                GameMatcher.ObstacleTypeId,
                GameMatcher.WorldPosition
            ).NoneOf(GameMatcher.ObstacleLocationsInitialized));
        }

        public void Execute()
        {
            foreach (GameEntity obstacleLocation in _obstacleLocations.GetEntities(_buffer))
            {
               int  numberOfObstacles = Random.Range(1, 4);
               HashSet<int> obstacleIds = new HashSet<int>();
               
               var minSpeed = Mathf.Lerp(1, 5, (int)obstacleLocation.WorldPosition.y / 500f);
               var maxSpeed = Mathf.Lerp(5, 10, (int)obstacleLocation.WorldPosition.y / 500f);
               var speed = Random.Range(minSpeed, maxSpeed);
               int dir = 2 * Random.Range(0, 2) - 1;
               
               for (int i = 0; i < numberOfObstacles; i++)
               {
                   CreateObstalce(obstacleLocation.ObstacleTypeId, 
                       (int)obstacleLocation.WorldPosition.y, 
                       speed,  
                       dir,
                       ref obstacleIds);
               }
               obstacleLocation.ReplaceObstacleLocations(obstacleIds);
               obstacleLocation.isObstacleLocationsInitialized = true;
            }
        }

        private void CreateObstalce(ObstacleTypeId typeId, int worldPositionY, float speed, int dir, ref HashSet<int> obstacleIds)
        {
            switch (typeId)
            {
                case ObstacleTypeId.Tree:
                case ObstacleTypeId.Currency:
                {
                    int xPosition = GetUniqueXPosition(ref obstacleIds, -3, 3);
                    Vector3 position = new Vector3(xPosition, worldPositionY, 0);
            
                    obstacleIds.Add(xPosition);
                    _obstacleFactory.CreateObstacle(typeId, position);
                    break;
                }
                case ObstacleTypeId.Car:
                {
                    float gap = Random.Range(2f, 3f);
                    Vector3 position = new Vector3(dir == -1 ? (5 * 3f) - dir : (-5 * 3f) - dir, worldPositionY, 0);
                    while (obstacleIds.Contains((int)position.x))
                    {
                        gap = Random.Range(5f, 10f);
                        position = new Vector3(dir == -1 ? (5 * gap) - dir : (-5 * gap) - dir, worldPositionY, 0);
                    }
                    obstacleIds.Add((int)position.x);

                    GameEntity car = _obstacleFactory.CreateObstacle(ObstacleTypeId.Car, position);
                    car.ReplaceDirection(new Vector2(dir, 0))
                        .ReplaceSpeed(speed)
                        .ReplaceInitialeWorldPosition(position);
                    break;
                }
            }
        }

        private int GetUniqueXPosition(ref HashSet<int> usedPositions, int min, int max)
        {
            const int maxAttempts = 10;
            for (int i = 0; i < maxAttempts; i++)
            {
                int candidate = Random.Range(min, max + 1);
                if (!usedPositions.Contains(candidate))
                    return candidate;
            }
            foreach (int x in System.Linq.Enumerable.Range(min, max - min + 1))
            {
                if (!usedPositions.Contains(x))
                    return x;
            }
            return min;
        }
    }
}