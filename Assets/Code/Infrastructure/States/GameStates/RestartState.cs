using System.Collections;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.HUD.LoadingWindow;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States.GameStates
{
    public class RestartState : EndOfFrameExitState
    {
        private readonly MetaContext _meta;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IBattleFeatureService _battleFeatureService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingController _loadingController;
        private readonly GameContext _gameContext;

        public RestartState(MetaContext meta,
            IStaticWindowService staticWindowService,
            ISaveLoadService saveLoadService, 
            IBattleFeatureService battleFeatureService,
            IGameStateMachine gameStateMachine,
            ICoroutineRunner coroutineRunner,
            LoadingController loadingController)
        {
            _meta = meta;
            _saveLoadService = saveLoadService;
            _battleFeatureService = battleFeatureService;
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
            _loadingController = loadingController;
        }

        public override void Enter()
        {
            _coroutineRunner.StartCoroutine(RestartGame(_meta));
        }

        private IEnumerator RestartGame(MetaContext meta)
        {
            _loadingController.Show();
            SetCurrencyToUI(meta);
            yield return null;
            _battleFeatureService.Deactivate();
            _saveLoadService.SaveProgress();
            _gameStateMachine.Enter<LoadingBattleState, string>(SceneManager.GetActiveScene().name);
        }

        protected override void ExitOnEndOfFrame()
        {

        }
        private void SetCurrencyToUI(MetaContext meta)
        {
            MetaEntity scoreStorage = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SessionScore
            )).GetSingleEntity();
            scoreStorage.ReplaceSessionScore(0);
            
            MetaEntity currencyStorage = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SessionCurrency
            )).GetSingleEntity();
            currencyStorage.ReplaceSessionCurrency(0);
        }
    }
}