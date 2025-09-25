using Code.Gameplay.Cards.Configs;

namespace Code.Gameplay.Cards.Factories
{
    public interface ICardFactory
    {
        GameEntity CreateCard(GameEntity cell, CardTypeId cardTypeId);
    }
}