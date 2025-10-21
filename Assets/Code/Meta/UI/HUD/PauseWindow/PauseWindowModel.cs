using System.Threading.Tasks;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.HUD.LoadingWindow;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;

namespace Code.Meta.UI.HUD.PauseWindow
{
    public class PauseWindowModel
    {
        private readonly IAudioFactory _audioFactory;
        private readonly IStaticWindowService _staticWindowService;
        private readonly IBattleFeatureService _battleFeatureService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUpdatableWindowService _updatableWindowService;
        private readonly LoadingController _loadingController;
        private readonly MetaContext _meta;
        private readonly StaticWindowId _id;

        public PauseWindowModel(MetaContext meta,
            StaticWindowId staticWindowId,
            IAudioFactory audioFactory, 
            IStaticWindowService staticWindowService, 
            IBattleFeatureService battleFeatureService, 
            IGameStateMachine gameStateMachine,
            IUpdatableWindowService updatableWindowService,
            LoadingController loadingController)
        {
            _meta = meta;
            _id = staticWindowId;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
            _battleFeatureService = battleFeatureService;
            _gameStateMachine = gameStateMachine;
            _updatableWindowService = updatableWindowService;
            _loadingController = loadingController;
        }
        
        public async void ReturnHome()
        {
            CustomDebug.LogWarning("Dont forget to close windows on ReturnHome");
            _loadingController.Show();
            _staticWindowService.CloseAll();
            _updatableWindowService.CloseAll();
            SetCurrencyToUI(_meta);
            
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(_id);
            await Task.Delay(100);
            _battleFeatureService.Deactivate();
            _gameStateMachine.Enter<LoadingHomeScreenState>();
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
        public void Continue()
        {
            _staticWindowService.Close(_id);
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _gameStateMachine.Enter<BattleLoopState>();
        }

        public void Restart()
        {
            CustomDebug.LogWarning("Dont forget to close windows on Restart");
            _staticWindowService.CloseAll();
            _updatableWindowService.CloseAll();
            _gameStateMachine.Enter<RestartState>();
        }
    }
}