using Code.Gameplay.Eggs.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Eggs.Systems
{
    public class SpawnEggOnTimerSystem: IExecuteSystem
    {
        private readonly IEggFactory _eggFactory;
        private readonly IGroup<GameEntity> _timers;

        public SpawnEggOnTimerSystem(GameContext game, IEggFactory eggFactory)
        {
            _eggFactory = eggFactory;
            _timers = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.EggCurrentSpawnTime,
                GameMatcher.EggSpawnActive
            ));
        }

        public void Execute()
        {
            foreach (GameEntity timer in _timers)
            {
                timer.ReplaceEggCurrentSpawnTime(timer.EggCurrentSpawnTime - Time.deltaTime);
                if (timer.EggCurrentSpawnTime <= 0)
                {
                    _eggFactory.GetEgg(timer.WorldPosition);
                    timer.ReplaceEggCurrentSpawnTime(timer.EggMaxSpawnTime);
                }
            }
        }
    }
}