using System.Collections.Generic;
using UnityEngine;

namespace Code.Meta.Levels.Configs
{
    [CreateAssetMenu(menuName = "Custom/Levels/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfigs: ScriptableObject
    {
        public List<LevelData> Levels;
    }
}