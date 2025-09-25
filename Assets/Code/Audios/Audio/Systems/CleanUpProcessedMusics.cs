using System.Collections.Generic;
using Entitas;

namespace Code.Audios.Audio.Systems
{
    public class CleanUpProcessedMusics: ICleanupSystem
    {
        private readonly IGroup<AudioEntity> _musics;
        private readonly List<AudioEntity> _buffer = new(1);

        public CleanUpProcessedMusics(AudioContext context)
        {
            _musics = context.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.Music,
                AudioMatcher.Processed
            ));
        }

        public void Cleanup()
        {
            foreach (AudioEntity music in _musics.GetEntities(_buffer))
            {
                music.Destroy();
            }
        }
    }
}