using System.Collections.Generic;
using Entitas;

namespace Code.Audios.Audio.Systems
{
    public class CleanUpProcessedSounds: ICleanupSystem
    {
        private readonly IGroup<AudioEntity> _sounds;
        private readonly List<AudioEntity> _buffer = new(1);

        public CleanUpProcessedSounds(AudioContext context)
        {
            _sounds = context.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.Sound,
                AudioMatcher.Processed
            ));
        }

        public void Cleanup()
        {
            foreach (AudioEntity sound in _sounds.GetEntities(_buffer))
            {
                sound.Destroy();
            }
        }
    }
}