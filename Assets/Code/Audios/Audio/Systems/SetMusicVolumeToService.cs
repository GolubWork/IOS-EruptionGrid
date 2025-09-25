using Code.Meta.UI.HUD.SettingsWindow.Services;
using Entitas;
using UnityEngine;

namespace Code.Audios.Audio.Systems
{
    public class SetMusicVolumeToService : IExecuteSystem
    {
        private readonly ISettingsService _settingsService;
        private readonly IGroup<AudioEntity> _musicSources;

        public SetMusicVolumeToService(AudioContext auidioContext, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _musicSources = auidioContext.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.MusicSource,
                AudioMatcher.Volume
            ));
        }

        public void Execute()
        {
            foreach (AudioEntity musicSource in _musicSources)
            {
                if (!Mathf.Approximately(musicSource.Volume, _settingsService.GetCurrentMusicVolume()))
                {
                    _settingsService.SetCurrentMusicVolume(musicSource.Volume);
                }
            }
        }
    }    
}