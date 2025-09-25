using Code.Gameplay.Buildings.Configs;
using UnityEngine;

namespace Code.Gameplay.ConveyorBelt.Factories
{
    public interface IConveyourBeltFactory
    {
        GameEntity CreateBelt(Vector3 at, Vector3 startPoint, Vector3 endPoint, BuildingTypeId buildingTypeId);
    }
}