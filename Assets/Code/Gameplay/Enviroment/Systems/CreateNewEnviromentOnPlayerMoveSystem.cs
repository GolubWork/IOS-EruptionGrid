using UnityEngine;
using Code.Gameplay.Enviroment.Configs;
using Code.Gameplay.Enviroment.Factories;
using Entitas;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Enviroment.Systems
{
    public class CreateNewEnviromentOnPlayerMoveSystem : IExecuteSystem
    {
        private readonly IEnviromentFactory _enviromentFactory;
        private readonly IGroup<GameEntity> _players;

        private float _spawnStep = 1f;      
        private const int InitialSpawnCount = 30;    
        private int _nextSpawnY;
        

        public CreateNewEnviromentOnPlayerMoveSystem(GameContext context, IEnviromentFactory enviromentFactory)
        {
            _enviromentFactory = enviromentFactory;
            _players = context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.WorldPosition));
            _nextSpawnY = -13 + InitialSpawnCount;
        }

        public void Execute()
        {
            foreach (var player in _players)
            {
                float currentY = player.WorldPosition.y;

                if (currentY < _nextSpawnY - 15)
                    continue;

                Vector3 spawnPosition = new Vector3(0, _nextSpawnY, 0);
                CreateEnviromentAt(spawnPosition);

                _nextSpawnY++; 
            }
        }

        private void CreateEnviromentAt(Vector3 position)
        {
            float roadProbability = Mathf.Lerp(0.5f, 0.9f, position.y / 250f);

            EnviromentTypeId typeId;
            if (Random.value < roadProbability)
                typeId = EnviromentTypeId.Road;
            else
                typeId = EnviromentTypeId.Grass;

            _enviromentFactory.CreateEnviroment(typeId, position);
        }
    }
}
