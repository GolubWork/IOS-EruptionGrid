using System.Collections.Generic;
using Entitas;

namespace Code.Audios.Audio.Systems
{
    public class ChangeSoundVolumeSettingSystem : IExecuteSystem
    {
        private readonly IGroup<AudioEntity> _soundProducers;
        private readonly IGroup<AudioEntity> _volumeChangers;
        private List<AudioEntity> _buffer = new (1);

        public ChangeSoundVolumeSettingSystem(AudioContext contextParameter)
        {
            _soundProducers = contextParameter.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.SoundSource
            ));

            _volumeChangers = contextParameter.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.SoundVolumeChanger,
                AudioMatcher.Volume
            ));
        }

        public void Execute()
        {
            foreach (AudioEntity producer in _soundProducers)
            foreach (AudioEntity volumeChanger in _volumeChangers.GetEntities(_buffer))
            {
                producer.ReplaceVolume(volumeChanger.Volume);
                volumeChanger.Destroy();
            }
        }
    }
}