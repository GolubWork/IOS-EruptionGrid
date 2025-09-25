using System.Collections.Generic;
using Code.Audios.Audio.Configs;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.AssetManagement.Constants;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Audios.Audio.Services
{
    public class AudioService : IAudioService
    {
        private Dictionary<MusicTypeId, AudioClip> _musicsById = new Dictionary<MusicTypeId, AudioClip>();
        private Dictionary<SoundTypeId, AudioClip> _soundsById = new Dictionary<SoundTypeId, AudioClip>();

        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(
            IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }


        public async UniTask LoadAll()
        {
            await LoadMusics();
            await LoadSounds();
        }

        public async UniTask LoadMusics()
        {
            MusicConfig config =
                await _assetProvider.LoadScriptable<MusicConfig>(ConfigsDirectoryConstants.MusicConfigSourcePath);

            if (config.Musics.Length == 0) return;
            foreach (Music music in config.Musics)
            {
                _musicsById.Add(music.Type, music.AudioClip);
            }
        }

        public async UniTask LoadSounds()
        {
            SoundsConfig config = 
                await _assetProvider.LoadScriptable<SoundsConfig>(ConfigsDirectoryConstants.SoundConfigSourcePath);
            
            if (config.Sounds.Length == 0) return;
            foreach (Sound sound in config.Sounds)
            {
                _soundsById.Add(sound.Type, sound.AudioClip);
            }
        }

        public AudioClip GetMusic(MusicTypeId musicId)
        {
            if (_musicsById.TryGetValue(musicId, out AudioClip audioClip))
                return audioClip;
            Debug.LogWarning($"Music clip for {musicId} was not found");
            return null;
        }

        public AudioClip GetSound(SoundTypeId soundId)
        {
            if (_soundsById.TryGetValue(soundId, out AudioClip audioClip))
                return audioClip;
            Debug.LogWarning($"Sound clip for {soundId} was not found");
            return null;
        }
    }
}