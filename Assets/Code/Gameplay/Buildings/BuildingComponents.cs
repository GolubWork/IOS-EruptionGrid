using Code.Gameplay.Buildings.Configs;
using Code.Gameplay.Items.Configs;
using Entitas;

namespace Code.Gameplay.Buildings
{
    [Game] public class Building : IComponent { }
    [Game] public class BuildingTypeIdComponent : IComponent { public BuildingTypeId Value; }
    [Game] public class RecievingItemsTypeId : IComponent { public ProducingItemTypeId[] Value; }
    [Game] public class ProducingItemsTypeId : IComponent { public ProducingItemTypeId[] Value; }
    [Game] public class ProducingSpeed : IComponent { public float Value; }
    [Game] public class MaxBuildingStorage : IComponent { public int Value; }
    [Game] public class CurBuildingStorage : IComponent { public int Value; }
    
}