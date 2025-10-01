using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Common.Helpers;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Windows.StaticWindows;

namespace Code.Meta.UI.HUD.HomeWindow
{
    public class HomeModel
    {
        private IGameStateMachine _stateMachine;
        private IAudioFactory _audioFactory;
        private IStaticWindowService _staticWindowService;


        public HomeModel(
            IGameStateMachine gameStateMachine,
            IAudioFactory audioFactory,
            IStaticWindowService staticWindowService
           )
        {
            _stateMachine = gameStateMachine;
            _audioFactory = audioFactory;
            _staticWindowService = staticWindowService;
        }

        public void EnterBattleLoadingState()
        {
            CustomDebug.Log("Enter BattleLoadingState");
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _stateMachine.Enter<LoadingBattleState, string>(MetaConstants.BattleSceneName);
            _staticWindowService.CloseAll();
          
        }

        public void Settings()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.HomeWindow);
            _staticWindowService.Open(StaticWindowId.SettingsWindow);
        }
        
        public void Achivments()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.HomeWindow);
            _staticWindowService.Open(StaticWindowId.AchivmentsWindow);
        }
        
        public void Shop()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.HomeWindow);
            _staticWindowService.Open(StaticWindowId.ShopWindow);
        }

        public void Privacy()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.HomeWindow);
            _staticWindowService.Open(StaticWindowId.PrivacyWindow);
        }

        public void Levels()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.HomeWindow);
            _staticWindowService.Open(StaticWindowId.LevelSelectionWindow);
        }

        public void Leaderboard()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(StaticWindowId.HomeWindow);
            _staticWindowService.Open(StaticWindowId.LeaderBoardWindow);
        }
    }
}