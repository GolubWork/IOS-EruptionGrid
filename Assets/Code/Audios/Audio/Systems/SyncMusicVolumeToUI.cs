using Code.Meta.UI.HUD.SettingsWindow;
using Code.Meta.UI.HUD.SettingsWindow.Services;
using Entitas;
using UnityEngine;

namespace Code.Audios.Audio.Systems
{
    public class SyncMusicVolumeToUI : IExecuteSystem
    {
        private readonly ISettingsService _settingsService;
        private readonly IGroup<AudioEntity> _musicSources;


        public SyncMusicVolumeToUI(AudioContext auidioContext, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _musicSources = auidioContext.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.MusicSource,
                AudioMatcher.Volume
                ));
        }

        public void Execute()
        {
            if (_settingsService.IsInitializing || _settingsService.IsUserInteracting) return;
            foreach (AudioEntity musicSource in _musicSources)
            {
                ISettingsController controller = _settingsService?.GetAudioSettingsController();
                
                float currentVolume = musicSource.Volume;
                if (controller == null) continue;
                float controllerVolume = _settingsService.GetCurrentMusicVolume();
                
                if (!Mathf.Approximately(currentVolume, controllerVolume))
                {
                    controller.SetMusicVolume(controllerVolume);
                }
            }
        }
    }
}