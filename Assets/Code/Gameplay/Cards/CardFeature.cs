using Code.Gameplay.Cards.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Cards
{
    public class CardFeature: Feature
    {
        public CardFeature(ISystemFactory systems)
        {
            Add(systems.Create<CreateCardsSystem>());
            Add(systems.Create<SetSpriteByIdSystem>());
           // Add(systems.Create<CheckCardTypesSystem>());
        }
    }
}