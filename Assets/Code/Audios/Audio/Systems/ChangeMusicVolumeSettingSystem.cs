using System.Collections.Generic;
using Entitas;

namespace Code.Audios.Audio.Systems
{
    public class ChangeMusicVolumeSettingSystem : IExecuteSystem
    {
        private readonly IGroup<AudioEntity> _musicProducers;
        private readonly IGroup<AudioEntity> _volumeChangers;
        private List<AudioEntity> _buffer = new (1);

        public ChangeMusicVolumeSettingSystem(AudioContext contextParameter)
        {
            _musicProducers = contextParameter.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.MusicSource
                ));

            _volumeChangers = contextParameter.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.MusicVolumeChanger,
                AudioMatcher.Volume
            ));
        }

        public void Execute()
        {
            foreach (AudioEntity producer in _musicProducers)
            foreach (AudioEntity volumeChanger in _volumeChangers.GetEntities(_buffer))
            {
                producer.ReplaceVolume(volumeChanger.Volume);
                volumeChanger.Destroy();
            }
        }
    }
}