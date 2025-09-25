using UnityEngine;

namespace Code.Audios.Audio.Configs
{
    [CreateAssetMenu(menuName = "Custom/Audio/Sound Config", fileName = "SoundConfig")]
    public class SoundsConfig: ScriptableObject
    {
        public Sound[] Sounds;
    }
}