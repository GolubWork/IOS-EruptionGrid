using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;

namespace Code.Meta.UI.HUD.SettingsWindow.SwitchSettings
{
    public class ToggleSettingsModel
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAudioFactory _audioFactory;
        private readonly IStaticWindowService _staticWindowService;

        private bool _isMusicActive = true;
        private bool _isSoundActive = true;
        
        public ToggleSettingsModel(ISaveLoadService saveLoadService,IAudioFactory audioFactory, IStaticWindowService staticWindowService)
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

        public float ChangeMusicVolumeBySwitch()
        {
            _isMusicActive = !_isMusicActive;
            return _isMusicActive ? 1 : 0;
        }

        public float ChangeSoundVolumeBySwitch()
        {
            _isSoundActive = !_isSoundActive;
            return _isSoundActive ? 1 : 0;
        }

        public void SetMusicVolume(float volume) => 
            _audioFactory.CreateMusicVolumeChanger(volume);        
        
        public void SetAudioVolume(float volume) => 
            _audioFactory.CreateSoundVolumeChanger(volume);
        
    }
}