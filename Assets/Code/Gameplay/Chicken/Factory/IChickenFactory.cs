using UnityEngine;

namespace Code.Gameplay.Chicken.Factory
{
    public interface IChickenFactory
    {
       GameEntity CreateHero(Vector3 at);
    }
}