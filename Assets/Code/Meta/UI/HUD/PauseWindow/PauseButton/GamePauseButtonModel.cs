using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;

namespace Code.Meta.UI.HUD.PauseWindow.PauseButton
{
    public class GamePauseButtonModel
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IAudioFactory _audioFactory;

        public GamePauseButtonModel(IGameStateMachine stateMachine, IAudioFactory audioFactory)
        {
            _gameStateMachine = stateMachine;
            _audioFactory = audioFactory;
        }

        public void Pause()
        {
            if (_gameStateMachine.CompareState<GamePauseState>() || _gameStateMachine.CompareState<GameOverState>()) return;
            _audioFactory.CreateSound(SoundTypeId.BtnClick);
            _gameStateMachine.Enter<GamePauseState>();
        }
    }
}