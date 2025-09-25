using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.SettingsWindow.SwitchSettings
{
    public class ToggleSettingsView
    {
        private Button _btnToggleMusic;
        private Button _btnToggleSound;

        private readonly Sprite _activeSprite;
        private readonly Sprite _inactiveSprite;

        public ToggleSettingsView(
            Button btnToggleMusic, 
            Button btnToggleSound, 
            Sprite activeSprite,
            Sprite inactiveSprite)
        {
            _btnToggleMusic = btnToggleMusic;
            _btnToggleSound = btnToggleSound;
            _activeSprite = activeSprite;
            _inactiveSprite = inactiveSprite;
        }


        public void ChangeMusicToggleViewByVolume(float volume)
        {
            bool isMusicOn = (int)volume == 1;
            _btnToggleMusic.GetComponent<Image>().sprite = isMusicOn
                ? _activeSprite
                : _inactiveSprite;
        }         
        public void ChangeSoundToggleViewByVolume(float volume)
        {
            bool isMusicOn = (int)volume == 1;
            _btnToggleSound.GetComponent<Image>().sprite = isMusicOn
                ? _activeSprite
                : _inactiveSprite;
        } 
    }
}