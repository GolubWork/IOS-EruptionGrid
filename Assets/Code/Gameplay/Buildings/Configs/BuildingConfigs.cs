using UnityEngine;

namespace Code.Gameplay.Buildings.Configs
{
    [CreateAssetMenu(menuName = "Custom/Buildings/BuildingConfigs", fileName = "BuildingConfigs")]
    public class BuildingConfigs: ScriptableObject
    {
        public BuildingConfig[] Buildings;
    }
}