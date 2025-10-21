using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.HUD.LoadingWindow;
using Code.Windows.StaticWindows;

namespace Code.Meta.UI.HUD.GameOverWindow
{
    public class GameOverWindowModel
    {
        private readonly StaticWindowId _id;
        private IGameStateMachine _gameStateMachine;
        private IStaticWindowService _staticWindowService;
        private IAudioFactory _audioFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingController _loadingController;

        public GameOverWindowModel(
            StaticWindowId id,
            IGameStateMachine stateMachine, 
            IStaticWindowService staticWindowService, 
            IAudioFactory audioFactory,
            ISceneLoader sceneLoader,
            LoadingController loadingController)
        {
            _id = id;
            _gameStateMachine = stateMachine;
            _staticWindowService = staticWindowService;
            _audioFactory = audioFactory;
            _sceneLoader = sceneLoader;
            _loadingController = loadingController;
        }

        public void ReturnHome()
        {
            _loadingController.Show();
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(_id);
            _gameStateMachine.Enter<LoadingHomeScreenState>();
        }

        public void Restart()
        {
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _staticWindowService.Close(_id);
            _gameStateMachine.Enter<RestartState>();
        }
    }
}