using System.Collections.Generic;
using UnityEngine;

namespace Code.Windows.UpdatableWindows.Configs
{
    [CreateAssetMenu(fileName = "UpdatableWindowConfig", menuName = "Custom/UI/Updatable Window Config")]
    public class UpdatableWindowsConfig: ScriptableObject
    {
        public GameObject canvasPrefab;
        public List<UpdatableWindowConfig> windowConfigs;
    }
}