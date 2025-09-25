using Code.Audios.Audio.Factory;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.StateMachine;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.PauseWindow
{
    public class PauseWindowController : StaticWindow
    {
        [SerializeField] private Button ReturnHomeButton;
        [SerializeField] private Button ContinueButton;
        [SerializeField] private Button btnRestart;

        private PauseWindowModel _model;

        [Inject]
        private void Construct(
            IGameStateMachine stateMachine, 
            IStaticWindowService staticWindowService, 
            IUpdatableWindowService updatableWindowService,
            IAudioFactory audioFactory, 
            IBattleFeatureService battleFeatureService)
        {
            Id = StaticWindowId.PauseWindow;
            _model = new PauseWindowModel(Id, audioFactory, staticWindowService, battleFeatureService, stateMachine, updatableWindowService);
        }

        protected override void Initialize()
        {
            ReturnHomeButton.onClick.AddListener(_model.ReturnHome);
            ContinueButton.onClick.AddListener(_model.Continue);
            btnRestart.onClick.AddListener(_model.Restart);
        }
        protected override void Cleanup()
        {
            ReturnHomeButton.onClick.RemoveListener(_model.ReturnHome);
            ContinueButton.onClick.RemoveListener(_model.Continue);
            btnRestart.onClick.RemoveListener(_model.Restart);
        }


    }
}