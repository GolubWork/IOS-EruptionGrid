using Entitas;

namespace Code.Audios.Audio.Systems
{
    public class SetVolumeToSource : IExecuteSystem
    {
        private readonly IGroup<AudioEntity> _musicSource;
        private readonly IGroup<AudioEntity> _soundSource;

        public SetVolumeToSource(AudioContext contextParameter)
        {
            _musicSource = contextParameter.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.MusicSource,
                AudioMatcher.Volume
                ));            
            
            _soundSource = contextParameter.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.SoundSource,
                AudioMatcher.Volume
                ));
        }

        public void Execute()
        {
            foreach (AudioEntity source in _musicSource)
            {
                source.MusicSource.volume = source.Volume;
            }            
            foreach (AudioEntity source in _soundSource)
            {
                source.SoundSource.volume = source.Volume;
            }
        }
    }

}