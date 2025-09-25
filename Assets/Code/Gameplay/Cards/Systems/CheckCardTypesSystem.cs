using System.Collections.Generic;
using Code.Common.Helpers;
using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Factory;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Input.Service;
using Entitas;

namespace Code.Gameplay.Cards.Systems
{
    public class CheckCardTypesSystem : IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IGroup<GameEntity> _cards;
        private List<GameEntity> _buffer = new(2);

        public CheckCardTypesSystem(GameContext context,
            IEffectFactory effectFactory,
            IEffectStaticDataService effectStaticDataService)
        {
            _effectFactory = effectFactory;
            _effectStaticDataService = effectStaticDataService;
            _cards = context.GetGroup(GameMatcher.AllOf(
                GameMatcher.Card,
                GameMatcher.CardTypeId,
                GameMatcher.ReadyToCheck
            ).NoneOf(GameMatcher.Tapable));
        }
        

        public void Execute()
        {
            if(_cards.GetEntities().Length < 2) return;
            List<GameEntity> cardsList = _cards.GetEntities(_buffer);
            
            CustomDebug.Log($"Card list: {cardsList.Count}");
            
            var firstCard = cardsList[0];
            var secondCard = cardsList[1];

            if (firstCard.CardTypeId == secondCard.CardTypeId)
            {
                UnityEngine.Debug.Log($"Карты совпадают: {firstCard.CardTypeId} : {secondCard.CardTypeId}");
                firstCard.isCollected = true;
                secondCard.isCollected = true;
            }
            else
            {
                UnityEngine.Debug.Log($"Карты разные: {firstCard.CardTypeId} и {secondCard.CardTypeId}");
                _effectFactory.CreateEffect(_effectStaticDataService.GetEffectConfig(EffectTypeId.FlipEffect),
                    firstCard.Id, firstCard.Id);
                _effectFactory.CreateEffect(_effectStaticDataService.GetEffectConfig(EffectTypeId.FlipEffect),
                    secondCard.Id, secondCard.Id);
                
                firstCard.isReadyToCheck = false;
                secondCard.isReadyToCheck = false;
            }

            _buffer.Clear();
            cardsList.Clear();
        }
    }
}