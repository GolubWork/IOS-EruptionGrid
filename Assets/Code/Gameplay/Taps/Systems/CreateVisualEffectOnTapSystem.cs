using System.Collections.Generic;
using Code.Gameplay.EffectsVisual.Factories;
using Code.Input.Service;
using Entitas;

namespace Code.Gameplay.Taps.Systems
{
    public class CreateVisualEffectOnTapSystem: IExecuteSystem
    {
        private readonly IVisualEffectFactory _visualEffectFactory;
        private readonly ITouchInputService _inputService;
        private readonly IGroup<GameEntity> _taped;
        private List<GameEntity> _buffer = new (1);

        public CreateVisualEffectOnTapSystem(GameContext game, 
            IVisualEffectFactory visualEffectFactory,
            ITouchInputService inputService)
        {
            _visualEffectFactory = visualEffectFactory;
            _inputService = inputService;
            _taped = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Taped,
                GameMatcher.TapVisualEffectConfig
            ));
        }

        public void Execute()
        {
            foreach (GameEntity taped in _taped.GetEntities(_buffer))
            {
                _visualEffectFactory.CreateVisualEffect(taped.TapVisualEffectConfig, taped.Id, taped.Id, _inputService.GetWorldMousePosition());
            }
        }
    }
}