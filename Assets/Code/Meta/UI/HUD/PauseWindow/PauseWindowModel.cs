using System.Threading.Tasks;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
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
        private readonly StaticWindowId _id;

        public PauseWindowModel(
            StaticWindowId staticWindowId,
            IAudioFactory audioFactory, 
            IStaticWindowService staticWindowService, 
            IBattleFeatureService battleFeatureService, 
            IGameStateMachine gameStateMachine,
            IUpdatableWindowService updatableWindowService)
        {
            _id = staticWindowId;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
            _battleFeatureService = battleFeatureService;
            _gameStateMachine = gameStateMachine;
            _updatableWindowService = updatableWindowService;
        }
        
        public async void ReturnHome()
        {
            CustomDebug.LogWarning("Dont forget to close windows on ReturnHome");
            _staticWindowService.CloseAll();
            _updatableWindowService.CloseAll();
            
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(_id);
            await Task.Delay(100);
            _battleFeatureService.Deactivate();
            _gameStateMachine.Enter<LoadingHomeScreenState>();
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