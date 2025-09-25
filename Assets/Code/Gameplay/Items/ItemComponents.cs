using Code.Gameplay.Items.Configs;
using Entitas;

namespace Code.Gameplay.Items
{
    [Game] public class Item : IComponent { }
    [Game] public class ItemTypeIdComponent : IComponent { public ProducingItemTypeId Value; }
}