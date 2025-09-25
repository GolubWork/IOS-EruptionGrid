using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Effects;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Entitas;

namespace Code.Gameplay.Zones.Systems
{
    public class AddZoneTapEffectSystem: IExecuteSystem
    {
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;
        private readonly IGroup<GameEntity> _zones;
        private List<GameEntity> _buffer = new (1);

        public AddZoneTapEffectSystem(GameContext game, IEffectStaticDataService effectStaticDataService, IVisualEffectStaticDataService visualEffectStaticDataService)
        {
            _effectStaticDataService = effectStaticDataService;
            _visualEffectStaticDataService = visualEffectStaticDataService;
            _zones = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Zone,
                GameMatcher.ZoneTypeId,
                GameMatcher.ZonePercent,
                GameMatcher.BoxCollider2D,
                GameMatcher.Transform
            ).NoneOf(GameMatcher.Tapable));
        }

        public void Execute()
        {
            foreach (GameEntity zone in _zones.GetEntities(_buffer))
            {
                zone
                    .AddTotalTapsRequired(1)
                    .AddTapsRequired(1)
                    .AddTapRapeatableTimes(-1)
                    .AddTapEffectConfig(_effectStaticDataService.GetEffectConfig(EffectTypeId.Tap))
                    .AddTapVisualEffectConfig(_visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.TapEffect))
                    .With(e => e.isTapable = true)
                    ;
            }
        }
        
        
    }
}