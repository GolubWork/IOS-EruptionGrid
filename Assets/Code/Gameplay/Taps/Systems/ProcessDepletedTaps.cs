using System.Collections.Generic;
using Code.Gameplay.Chicken.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Taps.Systems
{
    public class ProcessDepletedTaps : IExecuteSystem
    {
        private readonly IChickenFactory _chickenFactory;
        private readonly IGroup<GameEntity> _taped;
        private List<GameEntity> _buffer = new (1);

        public ProcessDepletedTaps(GameContext game, IChickenFactory chickenFactory)
        {
            _chickenFactory = chickenFactory;
            _taped = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TapRapeatableTimes,
                GameMatcher.TapDepleted,
                GameMatcher.TapsRequired,
                GameMatcher.TotalTapsRequired
            ));
        }

        public void Execute()
        {
            foreach (GameEntity taped in _taped.GetEntities(_buffer))
            {
                if (taped.TapRapeatableTimes > 0)
                {
                    taped.ReplaceTapRapeatableTimes(taped.TapRapeatableTimes - 1);
                    taped.ReplaceTapsRequired(taped.TotalTapsRequired);
                    taped.isTapDepleted = false;
                }
                else if (taped.TapRapeatableTimes == -1) // -1 means infinite
                {
                    taped.ReplaceTapsRequired(taped.TotalTapsRequired);

                    taped.isTapDepleted = false;
                }
                else
                {
                    _chickenFactory.CreateHero(taped.WorldPosition - new Vector3(0,0,1));
                    taped.isTapable = false;
                    taped.Destroy();// Logic for finishing the tapable object
                }
            }
        }
    }
}