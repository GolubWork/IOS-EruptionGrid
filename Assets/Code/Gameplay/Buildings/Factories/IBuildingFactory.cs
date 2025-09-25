using Code.Gameplay.Buildings.Configs;
using UnityEngine;

namespace Code.Gameplay.Buildings.Factories
{
    public interface IBuildingFactory
    {
        GameEntity Create(Vector3 at, BuildingTypeId buildingTypeId);
    }
}