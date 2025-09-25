using Code.Audios.Audio.Factory;
using Code.Meta.UI.HUD.SettingsWindow.Services;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.SettingsWindow.SliderSettings
{
    public class SliderSettingsWindow : StaticWindow, ISettingsController
    {
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider soundVolumeSlider;
        [SerializeField] private Button btnReturn;
        
        private SliderSettingModel _model;
        
        private IAudioFactory _audioFactory;
        private IStaticWindowService _staticWindowService;
        private ISettingsService _settingsService;
        
        private bool _isMusicSet;
        private bool _isSoundSet;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(
            IAudioFactory audioFactory, 
            IStaticWindowService staticWindowService,
            ISettingsService settingsService,
            ISaveLoadService saveLoadService)
        {
            Id = StaticWindowId.SettingsWindow;
            
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
            _settingsService = settingsService;
            _saveLoadService = saveLoadService;
        }

        protected override void Initialize()
        {
            _model = new SliderSettingModel(_saveLoadService,_audioFactory, _staticWindowService);
            _settingsService.SetAuidioSettingsController(this);
            
            float musicVolume = _settingsService.GetCurrentMusicVolume();
            float soundVolume = _settingsService.GetCurrentSoundVolume(); 
            
            SetMusicVolume(musicVolume);
            SetSoundVolume(soundVolume);
            
            musicVolumeSlider.value = musicVolume;
            soundVolumeSlider.value = soundVolume;
        }

        protected override void SubscribeUpdates()
        {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
            btnReturn.onClick.AddListener(OnReturn);
        }

        protected override void UnsubscribeUpdates()
        {
            musicVolumeSlider.onValueChanged.RemoveAllListeners();
            soundVolumeSlider.onValueChanged.RemoveAllListeners();
            btnReturn.onClick.RemoveAllListeners();
        }



        public void SetMusicVolume(float volume)
        {
            if(_isMusicSet) return;
            musicVolumeSlider.value = volume;
            _isMusicSet = true;
        }

        public void SetSoundVolume(float volume)
        {
            if(_isSoundSet) return;
            soundVolumeSlider.value = volume;
            _isSoundSet = true;
        }

        private void OnMusicVolumeChanged(float volume)
        {
            _model.ChangeMusicVolumeBySlider(volume);
        }

        private void OnSoundVolumeChanged(float volume)
        {
            _model.ChangeSoundVolumeBySlider(volume);
        }

        private void OnReturn()
        {
            _model.Home();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _settingsService.RemoveSettingsController();
            _isMusicSet = false;
            _isSoundSet = false;
        }
    }
}