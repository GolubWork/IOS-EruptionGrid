using UnityEngine;

namespace Code.Gameplay.Eggs.Factories
{
    public interface IEggFactory
    {
        GameEntity GetEgg(Vector3 at);
        void ReturnEgg(GameEntity egg);
    }
}