using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Audios.Audio.Factory
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IIdentifierService _identifierService;

        public AudioFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public AudioEntity CreateMusic(MusicTypeId musicTypeId)
        {
            if (musicTypeId == MusicTypeId.Unknown) return null;
            return CreateAudioEntity.Empty()
                .AddId(_identifierService.Next())
                .AddMusic(musicTypeId)
                .With(e => e.isProcessed = false);
                ;
        }

        public AudioEntity CreateSound(SoundTypeId soundTypeId)
        {
            if (soundTypeId == SoundTypeId.Unknown) return null;
            return CreateAudioEntity.Empty()
                .AddId(_identifierService.Next())
                .AddSound(soundTypeId)
                .With(e => e.isProcessed = false);
            ;
        }

        public AudioEntity CreateMusicSource(Vector3 at)
        {
            return CreateAudioEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddVolume(1)
                    .AddViewPath(PrefabsDirectoryConstants.MusicPrefabSourcePath)
                ;
        }
        public AudioEntity CreateSoundSource(Vector3 at)
        {
            return CreateAudioEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddVolume(1)
                    .AddViewPath(PrefabsDirectoryConstants.SoundPrefabSourcePath)
                ;
        }

        public AudioEntity CreateAuidioListener(Vector3 at)
        {
            return CreateAudioEntity.Empty()
                .AddId(_identifierService.Next())
                .AddWorldPosition(at)
                .AddViewPath(PrefabsDirectoryConstants.AudioListenerPrefabSourcePath);
        }
        
        
        public AudioEntity CreateMusicVolumeChanger(float volume)
        {
            return CreateAudioEntity.Empty()
                .AddId(_identifierService.Next())
                .AddVolume(volume)
                .With(e => e.isMusicVolumeChanger = true);
        }        
        
        public AudioEntity CreateSoundVolumeChanger(float volume)
        {
            return CreateAudioEntity.Empty()
                .AddId(_identifierService.Next())
                .AddVolume(volume)
                .With(e => e.isSoundVolumeChanger = true);
        }
        
    }
}