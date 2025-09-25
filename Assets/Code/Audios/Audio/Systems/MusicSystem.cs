using System.Collections.Generic;
using Code.Audios.Audio.Services;
using Entitas;

namespace Code.Audios.Audio.Systems
{
    public class MusicSystem : IExecuteSystem
    {
        private readonly IAudioService _audioService;
        private readonly IGroup<AudioEntity> _musics;
        private readonly IGroup<AudioEntity> _musicSources;
        private readonly List<AudioEntity> _buffer = new(1);

        public MusicSystem(AudioContext context, IAudioService audioService)
        {
            _audioService = audioService;
            _musics = context.GetGroup(AudioMatcher.AllOf(
                    AudioMatcher.Music
                )
                .NoneOf(AudioMatcher.Processed));

            _musicSources = context.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.MusicSource
            ));
        }

        public void Execute()
        {
            foreach (AudioEntity music in _musics.GetEntities(_buffer))
            foreach (AudioEntity source in _musicSources)
            {
                source.MusicSource.clip = _audioService.GetMusic(music.Music);
                source.MusicSource.Play();
                music.isProcessed = true;
            }
        }
    }
}