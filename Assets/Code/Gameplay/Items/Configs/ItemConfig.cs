using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;

namespace Code.Gameplay.Items.Configs
{
    [System.Serializable]
    public class ItemConfig
    {
        public ProducingItemTypeId typeId;
        public GameEntityBehaviour prefab;
    }
}