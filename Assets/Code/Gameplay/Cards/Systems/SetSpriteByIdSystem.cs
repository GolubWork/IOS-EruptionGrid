using System.Collections.Generic;
using Code.Gameplay.StaticData.cardsStaticData;
using Entitas;

namespace Code.Gameplay.Cards.Systems
{
    public class SetSpriteByIdSystem: IExecuteSystem
    {
        private readonly ICardSpriteStaticDataProvider _cardSpriteStaticDataProvider;
        private readonly IGroup<GameEntity> _cards;
        private List<GameEntity> _buffer = new (1);

        public SetSpriteByIdSystem(GameContext context, ICardSpriteStaticDataProvider cardSpriteStaticDataProvider)
        {
            _cardSpriteStaticDataProvider = cardSpriteStaticDataProvider;
            _cards = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Card,
                GameMatcher.CardSpriteRenderer,
                GameMatcher.RequireSpriteSet,
                GameMatcher.CardTypeId
            ));
        }

        public void Execute()
        {
            foreach (GameEntity card in _cards.GetEntities(_buffer))
            {
                card.CardSpriteRenderer.sprite = _cardSpriteStaticDataProvider.GetCardSpriteByID(card.CardTypeId);
                card.isRequireSpriteSet = false;
            }
        }
    }
}