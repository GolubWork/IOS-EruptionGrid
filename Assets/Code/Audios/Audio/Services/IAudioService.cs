using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Audios.Audio.Services
{
    public interface IAudioService
    {
        UniTask LoadAll();
        UniTask LoadMusics();
        UniTask LoadSounds();
        AudioClip GetMusic(MusicTypeId musicId);
        AudioClip GetSound(SoundTypeId soundId);
    }
}