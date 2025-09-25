namespace Code.Meta.UI.HUD.SettingsWindow.Services
{
    public interface ISettingsService
    {
        bool IsUserInteracting { get; set; }
        bool IsInitializing { get; set; }
        
        ISettingsController SetAuidioSettingsController(ISettingsController settingsController);
        void RemoveSettingsController();
        ISettingsController GetAudioSettingsController();
        float GetCurrentMusicVolume();
        float GetCurrentSoundVolume();
        float SetCurrentMusicVolume(float volume);
        float SetCurrentSoundVolume(float volume);
    }
}