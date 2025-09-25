using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Items.Configs
{
    [CreateAssetMenu(menuName = "Custom/Items/ItemConfigs", fileName = "ItemConfigs")]
    public class ItemConfigs: ScriptableObject
    {
        public List<ItemConfig> configs;
    }
}