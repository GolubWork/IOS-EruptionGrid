using UnityEngine;

namespace Code.Gameplay.Shelfs.Factories
{
    public interface IShelfFactory
    {
        GameEntity GetShelf(Vector3 at);
        void ReturnShelf(GameEntity shelf);
    }
}