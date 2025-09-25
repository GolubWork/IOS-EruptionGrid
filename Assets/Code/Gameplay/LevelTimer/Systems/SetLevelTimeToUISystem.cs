using Code.Meta.UI.HUD.TimerWindow.Servises;
using Entitas;

namespace Code.Gameplay.LevelTimer.Systems
{
    public class SetLevelTimeToUISystem: IExecuteSystem
    {
        private readonly ILevelTimerBarService _levelTimerBarService;
        private readonly IGroup<GameEntity> _timers;

        public SetLevelTimeToUISystem(GameContext game, ILevelTimerBarService levelTimerBarService)
        {
            _levelTimerBarService = levelTimerBarService;
            _timers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.LevelTimer,
                GameMatcher.CurrentTimer
            ));
        }

        public void Execute()
        {
            foreach (GameEntity timer in _timers)
            {
                _levelTimerBarService.GetTimerWindowController().UpdateTime(timer.CurrentTimer);
            }
        }
    }
}