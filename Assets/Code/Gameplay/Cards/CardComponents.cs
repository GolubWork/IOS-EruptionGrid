using Code.Gameplay.Cards.Configs;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Cards
{
    [Game] public class Card : IComponent { }
    [Game] public class CardTypeIdComponent : IComponent { public CardTypeId Value; }
    [Game] public class CardSpriteRenderer : IComponent { public SpriteRenderer Value; }
    [Game] public class FaceUp : IComponent { }
    [Game] public class Flipping : IComponent { }
    [Game] public class ReadyToCheck : IComponent { }
    [Game] public class ReturningCard : IComponent { }
    [Game] public class RequireSpriteSet : IComponent { }
    [Game] public class CardBackisActive : IComponent { }
}