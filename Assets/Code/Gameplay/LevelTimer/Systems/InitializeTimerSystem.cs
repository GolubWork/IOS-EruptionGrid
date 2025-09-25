using System;
using Entitas;

namespace Code.Gameplay.LevelTimer.Systems
{
    public class InitializeTimerSystem: IInitializeSystem
    {
        private readonly IGroup<GameEntity> _timers;
        private readonly IGroup<MetaEntity> _levels;

        public InitializeTimerSystem(GameContext game, MetaContext meta)
        {
            _timers = game.GetGroup(GameMatcher.LevelTimer);
            _levels = meta.GetGroup(MetaMatcher.ChosenLevel);
        }

        public void Initialize()
        {
            foreach (GameEntity timer in _timers)
            foreach (MetaEntity level in _levels)
            {
                timer.ReplaceMaxTimer(level.ChosenLevel.levelSeconds);
                timer.ReplaceCurrentTimer(timer.MaxTimer);
            }
        }
    }
}