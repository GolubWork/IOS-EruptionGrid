using Code.Meta.UI.HUD.SettingsWindow.Services;
using Entitas;
using UnityEngine;

namespace Code.Audios.Audio.Systems
{
    public class SetAudioVolumeToService : IExecuteSystem
    {
        private readonly ISettingsService _settingsService;
        private readonly IGroup<AudioEntity> _soundSources;

        public SetAudioVolumeToService(AudioContext auidioContext, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _soundSources = auidioContext.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.SoundSource,
                AudioMatcher.Volume
            ));
        }

        public void Execute()
        {
            foreach (AudioEntity soundSoruce in _soundSources)
            {
                if (!Mathf.Approximately(soundSoruce.Volume, _settingsService.GetCurrentSoundVolume()))
                {
                    _settingsService.SetCurrentSoundVolume(soundSoruce.Volume);
                }
            }
        }
    }
}