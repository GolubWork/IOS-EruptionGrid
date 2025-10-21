using UnityEngine;

namespace Code.Gameplay.Player.Factories
{
    public interface IPlayerFactory
    {
        GameEntity CreatePlayer(Vector3 at);
    }
}