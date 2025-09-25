using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States.GameStates
{
    public class RestartState : EndOfFrameExitState
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IBattleFeatureService _battleFeatureService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly GameContext _gameContext;

        public RestartState(
            IStaticWindowService staticWindowService,
            ISaveLoadService saveLoadService, 
            IBattleFeatureService battleFeatureService,
            IGameStateMachine gameStateMachine)
        {
            _saveLoadService = saveLoadService;
            _battleFeatureService = battleFeatureService;
            _gameStateMachine = gameStateMachine;
        }

        public override void Enter()
        {
            _battleFeatureService.Deactivate();
            _saveLoadService.SaveProgress();
            _gameStateMachine.Enter<LoadingBattleState, string>(SceneManager.GetActiveScene().name);
        }

        protected override void ExitOnEndOfFrame()
        {

        }

    }
}