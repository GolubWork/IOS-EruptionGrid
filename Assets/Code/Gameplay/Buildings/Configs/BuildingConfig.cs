using System.Collections.Generic;
using Code.Gameplay.Items.Configs;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;

namespace Code.Gameplay.Buildings.Configs
{
    [System.Serializable]
    public class BuildingConfig
    {
        public BuildingTypeId TypeId;
        public ProducingItemTypeId[] ProducingItems;
        public ProducingItemTypeId[] RecivingItems;
        public float ProducingSpeed;
        public int MaxStorage;
        public int CurStorage;
        public GameEntityBehaviour ViewPrefab;
    }
}