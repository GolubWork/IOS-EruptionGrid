using Code.Gameplay.Enviroment.Configs;
using Code.Gameplay.Enviroment.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Enviroment.Systems
{
    public class CreateEnviromentSystem: IInitializeSystem
    {
        private readonly IEnviromentFactory _enviromentFactory;
        private readonly int numberObInit = 30;

        public CreateEnviromentSystem(IEnviromentFactory enviromentFactory)
        {
            _enviromentFactory = enviromentFactory;
        }

        public void Initialize()
        {
            for (int i = 0; i < numberObInit; i++)
            {
                Vector3 spawnPosition = new Vector3(0, i - 13, 0);
                if (i < 6)
                {
                    _enviromentFactory.CreateHomeEnviroment(spawnPosition);
                    continue;
                }
                float roadProbability = Mathf.Lerp(0.5f, 0.9f, spawnPosition.y / 250f);
                if (Random.value < roadProbability)
                {
                    _enviromentFactory.CreateEnviroment(EnviromentTypeId.Road, spawnPosition);
                }
                else
                {
                    _enviromentFactory.CreateEnviroment(EnviromentTypeId.Grass, spawnPosition);
                }
            }
        }
    }
}