using Code.Audios.Audio.Factory;
using Code.Meta.UI.HUD.SettingsWindow.Services;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.SettingsWindow.SwitchSettings
{
    public class ToggleSettingsWindow : StaticWindow, ISettingsController
    {
        [SerializeField] private Button btnSwitchMusic;
        [SerializeField] private Button btnSwitchAudio;
        [SerializeField] private Button returnButton;

        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inActiveSprite;
        
    
        private IAudioFactory _audioFactory;
        private IStaticWindowService _staticWindowService;
        private ISettingsService _settingsService;
    
        private ToggleSettingsModel _model;
        private ToggleSettingsView _view;
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
            _model = new ToggleSettingsModel(_saveLoadService,_audioFactory, _staticWindowService);
            _view = new ToggleSettingsView(btnSwitchMusic, btnSwitchAudio, activeSprite, inActiveSprite);
            _settingsService.SetAuidioSettingsController(this);
            
            float musicVolume = _settingsService.GetCurrentMusicVolume();
            float soundVolume = _settingsService.GetCurrentSoundVolume(); 
            
            SetMusicVolume(musicVolume);
            SetSoundVolume(soundVolume);
            
            _view.ChangeMusicToggleViewByVolume(musicVolume);
            _view.ChangeSoundToggleViewByVolume(soundVolume);
        }
    
        protected override void SubscribeUpdates()
        {
            btnSwitchMusic.onClick.AddListener(OnSwitchMusicVolume);
            btnSwitchAudio.onClick.AddListener(OnSwitchAudioVolume);
            returnButton.onClick.AddListener(OnReturn);
        }

        protected override void UnsubscribeUpdates()
        {
            btnSwitchMusic.onClick.RemoveAllListeners();
            btnSwitchAudio.onClick.RemoveAllListeners();
            returnButton.onClick.RemoveAllListeners();
        }

        private void OnSwitchMusicVolume()
        {
            float volume = _model.ChangeMusicVolumeBySwitch();
            _view.ChangeMusicToggleViewByVolume(volume);
            SetMusicVolume(volume);
        }

        private void OnSwitchAudioVolume()
        {
            float volume = _model.ChangeSoundVolumeBySwitch();
            _view.ChangeSoundToggleViewByVolume(volume);
            SetSoundVolume(volume);
        }

        private void OnReturn()
        {
            _model.Home();
        }
        
        public void SetMusicVolume(float volume)
        {
            _model.SetMusicVolume(volume);
        }

        public void SetSoundVolume(float volume)
        {
            _model.SetAudioVolume(volume);
        }
    }
}
