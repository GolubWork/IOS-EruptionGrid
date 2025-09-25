using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;

namespace Code.Meta.UI.HUD.SettingsWindow.SliderSettings
{
    public class SliderSettingModel
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAudioFactory _audioFactory;
        private readonly IStaticWindowService _staticWindowService;
        
        public SliderSettingModel(ISaveLoadService saveLoadService,IAudioFactory audioFactory, IStaticWindowService staticWindowService)
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

        public void ChangeMusicVolumeBySlider(float volume)
        {
            _audioFactory.CreateMusicVolumeChanger(volume);
        }
        
        public void ChangeSoundVolumeBySlider(float volume)
        {
            _audioFactory.CreateSoundVolumeChanger(volume);
        }
        
    }
}