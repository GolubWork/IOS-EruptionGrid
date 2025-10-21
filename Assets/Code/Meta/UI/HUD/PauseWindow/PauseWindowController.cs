using Code.Audios.Audio.Factory;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.HUD.LoadingWindow;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;
using UnityEngine;
using UnityEngine.UI;

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
            MetaContext meta,
            IGameStateMachine stateMachine, 
            IStaticWindowService staticWindowService, 
            IUpdatableWindowService updatableWindowService,
            IAudioFactory audioFactory, 
            IBattleFeatureService battleFeatureService,
            LoadingController loadingController)
        {
            Id = StaticWindowId.PauseWindow;
            _model = new PauseWindowModel(meta,Id, audioFactory, staticWindowService, battleFeatureService, stateMachine, updatableWindowService, loadingController);
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