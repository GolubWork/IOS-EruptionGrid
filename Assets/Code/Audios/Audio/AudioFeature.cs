using Code.Audios.Audio.Systems;
using Code.Infrastructure.Systems;

namespace Code.Audios.Audio
{
    public class AudioFeature: Feature
    {
        public AudioFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeAudioSystem>());
            
            Add(systems.Create<MusicSystem>());
            Add(systems.Create<SoundSystem>());
            
            Add(systems.Create<SyncMusicVolumeToUI>());
            Add(systems.Create<SyncSoundVolumeToUI>());
            
            Add(systems.Create<ChangeMusicVolumeSettingSystem>());
            Add(systems.Create<ChangeSoundVolumeSettingSystem>());
            
            Add(systems.Create<SetVolumeToSource>());
            
            Add(systems.Create<SetMusicVolumeToService>());
            Add(systems.Create<SetAudioVolumeToService>());
            
            Add(systems.Create<CleanUpProcessedMusics>());
            Add(systems.Create<CleanUpProcessedSounds>());
            
            Add(systems.Create<CleanUpAudioSystem>());
        }
    }
}