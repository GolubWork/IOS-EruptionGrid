using Entitas;

namespace Code.Audios.Audio.Systems
{
    public class CleanUpAudioSystem : ICleanupSystem
    {
        private readonly IGroup<AudioEntity> _audioEntities;

        public CleanUpAudioSystem(Contexts contexts)
        {
            _audioEntities = contexts.audio.GetGroup(AudioMatcher
                .AllOf(
                    AudioMatcher.View,
                    AudioMatcher.Processed
                ));
        }

        public void Cleanup()
        {
            foreach (var audioEntity in _audioEntities.GetEntities())
            {
                audioEntity.isDestructed = true;
            }
        }
    }
}