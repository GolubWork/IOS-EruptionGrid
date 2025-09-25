using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using UnityEngine;

namespace Code.Meta.UI.HUD.SettingsWindow.DotSettings
{
    public class DotSettingsModel
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAudioFactory _audioFactory;
        private readonly IStaticWindowService _staticWindowService;
        private readonly float minVolume = 0;
        private readonly float maxVolume = 1;
        
        public DotSettingsModel(ISaveLoadService saveLoadService,IAudioFactory audioFactory, IStaticWindowService staticWindowService)
        {
            _saveLoadService = saveLoadService;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
        }
        
        public void Home()
        {
            _saveLoadService.SaveProgress();
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.SettingsWindow);
            _staticWindowService.Open(StaticWindowId.HomeWindow);
        }
        
        public float IncreaseVolume(int volumeDots, float currentVolume)
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            
            float step = 1f / volumeDots;
            currentVolume += step;
            currentVolume = Mathf.Clamp(currentVolume, minVolume, maxVolume);
            currentVolume = Mathf.Round(currentVolume * 10f) / 10f;
            return currentVolume;
        }
        public float DecreaseVolume(int volumeDots, float currentVolume)
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            
            float step = 1f / volumeDots;
            currentVolume -= step;
            currentVolume = Mathf.Clamp(currentVolume, minVolume, maxVolume);
            currentVolume = Mathf.Round(currentVolume * 10f) / 10f;
            return currentVolume;
        }
        
        public void ChangeMusicVolume(float volume)
        {
            _audioFactory.CreateMusicVolumeChanger(volume);
        }
        
        public void ChangeSoundVolume(float volume)
        {
            _audioFactory.CreateSoundVolumeChanger(volume);
        }
    }
}