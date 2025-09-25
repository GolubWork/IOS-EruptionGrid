using UnityEngine;

namespace Code.Gameplay.Abilities.Factory
{
    public interface IAbilityFXFactory
    {
        GameEntity CreateExplosiveFX(Vector3 at);
    }
}