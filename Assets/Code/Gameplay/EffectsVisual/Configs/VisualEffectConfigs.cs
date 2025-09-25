using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.EffectsVisual.Configs
{
    [CreateAssetMenu(menuName = "Custom/VisualEffects/VisualEffectConfigs", fileName = "VisualEffectConfigs")]
    public class VisualEffectConfigs: ScriptableObject
    {
        public List<VisualEffectConfig> configs;
    }
}