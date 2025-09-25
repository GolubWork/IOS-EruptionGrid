using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD
{
    public class GameplayHUD: MonoBehaviour
    {
        private IAudioFactory _audioFactory;
        private IStaticWindowService _staticWindowService;
        private IUpdatableWindowService _updatableWindowService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService,IAudioFactory audioFactory, IStaticWindowService staticWindowService, IUpdatableWindowService updatableWindowService)
        {
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
            _updatableWindowService = updatableWindowService;
        }
        
        private void Start()
        {
            _audioFactory.CreateMusic(MusicTypeId.GameTheme);
            _staticWindowService.Open(StaticWindowId.PauseButtonWindow);
            //_updatableWindowService.Open(UpdatableWindowId.TimerWindow);
            _updatableWindowService.Open(UpdatableWindowId.ResourceWindow);
            _updatableWindowService.Open(UpdatableWindowId.CurrentCurrencyWindow);
            _updatableWindowService.Open(UpdatableWindowId.CurrentScoreWindow);
        }

        private void OnApplicationQuit()
        {
            
        }
    }
}
