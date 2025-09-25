using Unity.Mathematics;
using UnityEngine;

namespace Code.Gameplay.Physics.Factories
{
    public interface IForceFactory
    {
        GameEntity CreateForce(int producerId, int targetId, Vector2 power);
    }
}