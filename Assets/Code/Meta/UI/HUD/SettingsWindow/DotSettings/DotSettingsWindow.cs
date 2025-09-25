using Code.Audios.Audio.Factory;
using Code.Meta.UI.HUD.SettingsWindow.Services;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.SettingsWindow.DotSettings
{
    public class DotSettingsWindow : StaticWindow, ISettingsController
    {
        [SerializeField] private Button btnMusicPlus, btnMusicMinus;
        [SerializeField] private Button btnSoundPlus, btnSoundMinus;
        [SerializeField] private Button btnReturn;

        [SerializeField] private GameObject[] musicDots;
        [SerializeField] private GameObject[] soundDots;


        private IAudioFactory _audioFactory;
        private IStaticWindowService _staticWindowService;
        private ISettingsService _settingsService;
        
        private DotSettingsModel _model;
        private DotSettingsView _view;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(
            IAudioFactory audioFactory,
            IStaticWindowService staticWindowService,
            ISettingsService settingsService,
            ISaveLoadService saveLoadService)
        {
            Id = StaticWindowId.SettingsWindow;

            _saveLoadService = saveLoadService;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
            _settingsService = settingsService;
        }
        public void SetMusicVolume(float volume)
        {
            _view.ChangeMusicDotsCapacityByVolume(volume);
            _model.ChangeMusicVolume(volume);
        }

        public void SetSoundVolume(float volume)
        {
            _view.ChangeSoundDotsCapacityByVolume(volume);
            _model.ChangeSoundVolume(volume);
        }
        
        protected override void Initialize()
        {
            _model = new DotSettingsModel(_saveLoadService,_audioFactory, _staticWindowService);
            _view = new DotSettingsView(musicDots, soundDots);
            
            _settingsService.SetAuidioSettingsController(this);
            
            float musicVolume = _settingsService.GetCurrentMusicVolume();
            float soundVolume = _settingsService.GetCurrentSoundVolume(); 
            
            SetMusicVolume(musicVolume);
            SetSoundVolume(soundVolume);
        }

        protected override void SubscribeUpdates()
        {
            btnMusicPlus.onClick.AddListener(OnMusicVolumePlus);
            btnMusicMinus.onClick.AddListener(OnMusicVolumeMinus);

            btnSoundPlus.onClick.AddListener(OnSoundVolumePlus);
            btnSoundMinus.onClick.AddListener(OnSoundVolumeMinus);

            btnReturn.onClick.AddListener(OnReturn);
        }
        protected override void UnsubscribeUpdates()
        {
            btnMusicPlus.onClick.RemoveAllListeners();
            btnMusicMinus.onClick.RemoveAllListeners();

            btnSoundPlus.onClick.RemoveAllListeners();
            btnSoundMinus.onClick.RemoveAllListeners();

            btnReturn.onClick.RemoveAllListeners();
        }
        
        private void OnMusicVolumePlus()=>
            SetMusicVolume(_model.IncreaseVolume(musicDots.Length, _settingsService.GetCurrentMusicVolume()));

        private void OnMusicVolumeMinus() =>
            SetMusicVolume(_model.DecreaseVolume(musicDots.Length, _settingsService.GetCurrentMusicVolume()));

        private void OnSoundVolumePlus() => 
            SetSoundVolume(_model.IncreaseVolume(soundDots.Length, _settingsService.GetCurrentSoundVolume()));

        private void OnSoundVolumeMinus() => 
            SetSoundVolume(_model.DecreaseVolume(soundDots.Length, _settingsService.GetCurrentSoundVolume()));
        private void OnReturn() => _model.Home();
    }
}