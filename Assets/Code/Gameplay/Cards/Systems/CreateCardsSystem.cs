using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Cards.Configs;
using Code.Gameplay.Cards.Factories;
using Entitas;

namespace Code.Gameplay.Cards.Systems
{
    public class CreateCardsSystem : IExecuteSystem
    {
        private readonly ICardFactory _cardFactory;
        private readonly IGroup<GameEntity> _gridCells;
        private List<GameEntity> _buffer = new(1);
        private Random _random = new();

        public CreateCardsSystem(GameContext context, ICardFactory cardFactory)
        {
            _cardFactory = cardFactory;

            // Создаём группу для ячеек, которые ещё не обработаны
            _gridCells = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.GridCell,
                GameMatcher.SpriteRenderer,
                GameMatcher.CardSpriteRenderer
            ).NoneOf(GameMatcher.Processed));
        }

        public void Execute()
        {
            List<GameEntity> cells = _gridCells.GetEntities(_buffer).ToList();
            int totalCards = cells.Count;

            if (totalCards % 2 != 0)
            {
                throw new InvalidOperationException("Количество ячеек для карт должно быть четным.");
            }
            int halfCount = totalCards / 2;
            List<CardTypeId> selectedCards = GenerateRandomCards(halfCount);
            List<CardTypeId> pairedCards = new List<CardTypeId>(selectedCards);
            selectedCards.AddRange(pairedCards);
            Shuffle(selectedCards);
            for (int i = 0; i < totalCards; i++)
            {
                _cardFactory.CreateCard(cells[i], selectedCards[i]);
                cells[i].isProcessed = true;
            }
        }

        private List<CardTypeId> GenerateRandomCards(int count)
        {
            Array cardTypes = Enum.GetValues(typeof(CardTypeId));
            List<CardTypeId> randomCards = new List<CardTypeId>(count);

            for (int i = 0; i < count; i++)
            {
                CardTypeId randomCard = (CardTypeId)cardTypes.GetValue(_random.Next(cardTypes.Length));
                randomCards.Add(randomCard);
            }

            return randomCards;
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = _random.Next(i + 1);
                (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
            }
        }
    }
}