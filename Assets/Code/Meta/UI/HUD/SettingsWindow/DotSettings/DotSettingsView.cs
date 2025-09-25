using UnityEngine;

namespace Code.Meta.UI.HUD.SettingsWindow.DotSettings
{
    public class DotSettingsView
    {
        private readonly GameObject[] _musicDots;
        private readonly GameObject[] _soundDots;

        public DotSettingsView(GameObject[] musicDots, GameObject[] soundDots)
        {
            _musicDots = musicDots;
            _soundDots = soundDots;
        }


        public void ChangeMusicDotsCapacityByVolume(float volume)
        {
            int activeDots = Mathf.FloorToInt(volume * _musicDots.Length);
            for (int i = 0; i < _musicDots.Length; i++)
            {
                _musicDots[i].SetActive(i < activeDots);
            }
        }


        public void ChangeSoundDotsCapacityByVolume(float volume)
        {
            int activeDots = Mathf.FloorToInt(volume * _soundDots.Length);
            for (int i = 0; i < _soundDots.Length; i++)
            {
                _soundDots[i].SetActive(i < activeDots);
            }
        }


        
        
    }
}