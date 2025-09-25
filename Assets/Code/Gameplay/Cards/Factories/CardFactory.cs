using Code.Common.Extensions;
using Code.Gameplay.Cards.Configs;
using Code.Gameplay.Effects;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;

namespace Code.Gameplay.Cards.Factories
{
    public class CardFactory : ICardFactory
    {
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;

        public CardFactory(
            IEffectStaticDataService effectStaticDataService, 
            IVisualEffectStaticDataService visualEffectStaticDataService
            )
        {
            _effectStaticDataService = effectStaticDataService;
            _visualEffectStaticDataService = visualEffectStaticDataService;
        }

        public GameEntity CreateCard(GameEntity cell, CardTypeId cardTypeId)
        {
            cell
                .AddCardTypeId(cardTypeId)
                .With(e => e.isCard = true)
                .With(e =>e.isRequireSpriteSet = true)
                
                .AddTotalTapsRequired(1)
                .AddTapsRequired(1)
                .AddTapRapeatableTimes(-1)
                .AddTapEffectConfig(_effectStaticDataService.GetEffectConfig(EffectTypeId.Tap))
                .AddTapVisualEffectConfig(_visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.TapEffect))
                .With(e => e.isTapable = true)
                
                .AddCollectEffect(_effectStaticDataService.GetEffectConfig(EffectTypeId.CollectEgg))
                .With(e => e.isCollectable = true)
                ;
            return cell;
        }
    }
}