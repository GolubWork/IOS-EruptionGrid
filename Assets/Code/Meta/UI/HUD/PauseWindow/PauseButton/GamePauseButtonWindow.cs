using Code.Audios.Audio.Factory;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.HUD.PauseWindow.PauseButton.Services;
using Code.Windows.StaticWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.PauseWindow.PauseButton
{
    public class GamePauseButtonWindow : StaticWindow
    {
        [SerializeField] private Button PauseButton;
    
        private GamePauseButtonModel _model;
        private IGamePauseButtonService _gamePauseButtonService;
        private IStaticWindowService _staticWindowService;

        [Inject]
        private void Construct(
            IStaticWindowService staticWindowService,
            IGameStateMachine stateMachine, 
            IAudioFactory audioFactory, 
            IGamePauseButtonService gamePauseButtonService)
        {
            Id = StaticWindowId.PauseButtonWindow;
            _model = new GamePauseButtonModel(stateMachine, audioFactory);
            _gamePauseButtonService = gamePauseButtonService;
            _staticWindowService = staticWindowService;
        }
    
        protected override void Initialize()
        {
            _gamePauseButtonService.SetGamePauseButton(this);
            PauseButton.onClick.AddListener(_model.Pause);
        }

        protected override void Cleanup()
        {
            _gamePauseButtonService.SetGamePauseButton(null);
            PauseButton.onClick.RemoveListener(_model.Pause);
            _staticWindowService.Close(Id);
        }
    }
}
