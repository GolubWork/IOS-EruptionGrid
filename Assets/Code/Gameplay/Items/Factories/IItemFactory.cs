using Code.Gameplay.Items.Configs;
using UnityEngine;

namespace Code.Gameplay.Items.Factories
{
    public interface IItemFactory
    {
        GameEntity CreateItem(Vector3 at, ProducingItemTypeId itemTypeId, int StartPoint, int EndPoint);
    }
}