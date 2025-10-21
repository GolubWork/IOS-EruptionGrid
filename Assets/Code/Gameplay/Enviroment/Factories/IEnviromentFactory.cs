using Code.Gameplay.Enviroment.Configs;
using UnityEngine;

namespace Code.Gameplay.Enviroment.Factories
{
    public interface IEnviromentFactory
    {
        GameEntity CreateEnviroment(EnviromentTypeId typeId, Vector3 at);
        GameEntity CreateGrassEnviroment(Vector3 at);
        GameEntity CreateHomeEnviroment(Vector3 at);
    }
}