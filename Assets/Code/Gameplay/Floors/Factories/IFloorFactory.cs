using UnityEngine;

namespace Code.Gameplay.Floors.Factories
{
    public interface IFloorFactory
    {
        GameEntity CreateFloor(Vector3 at);
    }
}