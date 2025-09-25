using System.Linq;
using Code.Audios.Audio.Configs;
using Code.Audios.Audio.Factory;
using Code.Progress.Provider;
using Code.Progress.SaveLoad;
using Zenject;

namespace Code.Meta.UI.HUD.SettingsWindow.Services
{
    public class SettingsService : ISettingsService
    {
        private ISettingsController _audioSettingsController;
        public bool IsUserInteracting { get; set; }
        public bool IsInitializing { get; set; }
        
        private float _currentMusicVolume;
        private float _currentSoundVolume;
        private ISaveLoadService _saveLoadService;
        private IProgressProvider _progressProvider;

        
        [Inject]
        private void Construct(IAudioFactory audioFactory, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            
            AudioSettingsData settings =  Contexts.sharedInstance.audio.GetGroup(AudioMatcher.AudioSettings).GetEntities().FirstOrDefault()?.AudioSettings;
            
            SetCurrentMusicVolume(settings!.MusicVolume);
            audioFactory.CreateMusicVolumeChanger(GetCurrentMusicVolume());
            
            SetCurrentSoundVolume(settings!.SoundVolume);
            audioFactory.CreateSoundVolumeChanger(GetCurrentSoundVolume());

        }
        
        public ISettingsController SetAuidioSettingsController(ISettingsController settingsController)
        {
            _audioSettingsController = settingsController;
            return _audioSettingsController;
        }

        public void RemoveSettingsController()
        {
            _audioSettingsController = null;
        }

        public ISettingsController GetAudioSettingsController()
        {
            return _audioSettingsController;
        }
        
        public float GetCurrentMusicVolume()
            => Contexts.sharedInstance.audio.GetGroup(AudioMatcher.AudioSettings).GetEntities().FirstOrDefault()!.AudioSettings.MusicVolume;

        public float GetCurrentSoundVolume()
            => Contexts.sharedInstance.audio.GetGroup(AudioMatcher.AudioSettings).GetEntities().FirstOrDefault()!.AudioSettings.SoundVolume;
           
        
        
        public float SetCurrentMusicVolume(float volume)
        {
            AudioSettingsData settings =  Contexts.sharedInstance.audio.GetGroup(AudioMatcher.AudioSettings).GetEntities().FirstOrDefault()?.AudioSettings;
            settings!.MusicVolume = volume;
            _currentMusicVolume = volume;
            _saveLoadService.SaveProgress();
            return _currentMusicVolume;
        }

        public float SetCurrentSoundVolume(float volume)
        {
            AudioSettingsData settings =  Contexts.sharedInstance.audio.GetGroup(AudioMatcher.AudioSettings).GetEntities().FirstOrDefault()?.AudioSettings;
            settings!.SoundVolume = volume;
            _currentSoundVolume = volume;
            _saveLoadService.SaveProgress();
            return _currentSoundVolume;
        }
    }
}