using Code.Meta.UI.HUD.SettingsWindow;
using Code.Meta.UI.HUD.SettingsWindow.Services;
using Entitas;
using UnityEngine;

namespace Code.Audios.Audio.Systems
{
    public class SyncSoundVolumeToUI : IExecuteSystem
    {
        private readonly ISettingsService _settingsService;
        private readonly IGroup<AudioEntity> _soundSources;


        public SyncSoundVolumeToUI(AudioContext auidioContext, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _soundSources = auidioContext.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.SoundSource,
                AudioMatcher.Volume
            ));
        }

        public void Execute()
        {
            if (!_settingsService.IsInitializing && !_settingsService.IsUserInteracting)
            {
                foreach (AudioEntity soundSource in _soundSources)
                {
                    float controllerVolume = _settingsService.GetCurrentSoundVolume();
                    ISettingsController controller = _settingsService.GetAudioSettingsController();
                    if (controller == null) continue;
                    
                    if (!Mathf.Approximately(soundSource.Volume, controllerVolume))
                    {
                        controller.SetSoundVolume(controllerVolume);
                    }
                }
            }
        }
    }
}