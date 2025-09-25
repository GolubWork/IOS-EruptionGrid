using Code.Audios.Audio;
using UnityEngine;

namespace Code.Gameplay.Effects.Configs
{
    [CreateAssetMenu(menuName = "Custom/Abilities/EffectConfig", fileName = "EffectConfig")]
    public class EffectConfig : ScriptableObject
    {
        public EffectTypeId EffectTypeId;
        public SoundTypeId EffectSound;
        public float Value;
    }
}