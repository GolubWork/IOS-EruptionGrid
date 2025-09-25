using System.Collections.Generic;
using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Factory;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.EffectsVisual.Factories;
using Code.Gameplay.Eggs.Factories;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Eggs.Systems
{
    public class ReturnEggOnLifeTimeSystem: IExecuteSystem
    {
        private readonly IEggFactory _eggFactory;
        private readonly IVisualEffectFactory _visualEffectFactory;
        private readonly IEffectFactory _effectFactory;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IGroup<GameEntity> _eggs;
        private List<GameEntity> _buffer = new (1);

        public ReturnEggOnLifeTimeSystem(GameContext game, 
            IEggFactory eggFactory, 
            IVisualEffectFactory visualEffectFactory, 
            IEffectFactory effectFactory,
            IEffectStaticDataService effectStaticDataService,
            IVisualEffectStaticDataService visualEffectStaticDataService)
        {
            _eggFactory = eggFactory;
            _visualEffectFactory = visualEffectFactory;
            _effectFactory = effectFactory;
            _effectStaticDataService = effectStaticDataService;
            _visualEffectStaticDataService = visualEffectStaticDataService;
            _eggs = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Egg,
                GameMatcher.EggLifeTime
            ));
        }

        public void Execute()
        {
            foreach (GameEntity egg in _eggs.GetEntities(_buffer))
            {
                egg.ReplaceEggLifeTime(egg.EggLifeTime - Time.deltaTime);
                if (egg.EggLifeTime <= 0)
                {
                    _visualEffectFactory.CreateVisualEffect(_visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.Collect), egg.Id, egg.Id, egg.WorldPosition);
                    _effectFactory.CreateEffect(_effectStaticDataService.GetEffectConfig(EffectTypeId.AddPoints), egg.Id, egg.Id);
                    _eggFactory.ReturnEgg(egg);
                }
            }
        }
    }
}