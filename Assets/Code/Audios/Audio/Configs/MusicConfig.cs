using UnityEngine;

namespace Code.Audios.Audio.Configs
{
    [CreateAssetMenu(menuName = "Custom/Audio/Music Config", fileName = "MusicConfig")]
    public class MusicConfig: ScriptableObject
    {
        public Music[] Musics;
    }
}