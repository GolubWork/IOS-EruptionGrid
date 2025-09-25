using System;
using Entitas;

namespace Code.Gameplay.LevelTimer.Systems
{
    public class DecreaseLevelTimerSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _timers;
        private float _elapsedTime;

        public DecreaseLevelTimerSystem(GameContext game)
        {
            _timers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.LevelTimer,
                GameMatcher.CurrentTimer
            ));
        }

        public void Execute()
        {
            _elapsedTime += UnityEngine.Time.deltaTime;
            if (_elapsedTime < 1f) return;
            foreach (GameEntity timer in _timers)
            {
                if (timer.CurrentTimer > 0)
                {
                    timer.ReplaceCurrentTimer(timer.CurrentTimer - 1);
                }
                else
                {
                    timer.isTimerCompleted = true;
                }
            }
            _elapsedTime = 0f;
        }
    }
}