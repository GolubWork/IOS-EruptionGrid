using Code.Common.Helpers;
using Code.Gameplay.Effects;
using Code.Gameplay.Effects.Factory;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Input.Service;
using DG.Tweening;

namespace Code.Gameplay.Cards.Helper
{
    public class CardComparer : ICardComparer
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly ITouchInputService _inputService;

        public GameEntity FirstCard = null;
        public GameEntity SecondCard = null;

        private bool _isProcessingPair = false;

        public CardComparer(
            IEffectFactory effectFactory, 
            IEffectStaticDataService effectStaticDataService, 
            ITouchInputService inputService)
        {
            _effectFactory = effectFactory;
            _effectStaticDataService = effectStaticDataService;
            _inputService = inputService;
        }

        public bool isFirstCardAsigned { get; set; } = false;

        public void SetFirstCard(GameEntity card)
        {
            FirstCard = card;
            isFirstCardAsigned = true;
            CustomDebug.Log($"Первая карта: {FirstCard.CardTypeId}");
        }

        public void SetSecondCard(GameEntity card)
        {
            if (_isProcessingPair)
            {
                CustomDebug.Log("Пара уже обрабатывается, нельзя открыть третью карту.");
                return;
            }

            _inputService.InputAvaliable = false;
            SecondCard = card;

            CustomDebug.Log($"Вторая карта: {SecondCard.CardTypeId}");

            bool cardsMatch = Compare();

            _isProcessingPair = true;

            if (cardsMatch)
            {
                UnityEngine.Debug.Log($"Карты совпадают: {FirstCard.CardTypeId} : {SecondCard.CardTypeId}");
                FirstCard.isCollected = true;
                SecondCard.isCollected = true;
                
                EndProcessing();
            }
            else
            {
                UnityEngine.Debug.Log($"Карты разные: {FirstCard.CardTypeId} и {SecondCard.CardTypeId}");

                CreateFlipEffect(FirstCard);
                CreateFlipEffect(SecondCard);

                FirstCard.isReadyToCheck = false;
                SecondCard.isReadyToCheck = false;

                FirstCard.isReturningCard = true;
                SecondCard.isReturningCard = true;

                DOTween.Sequence()
                    .AppendInterval(1.0f)
                    .OnComplete(EndProcessing);
            }

            FirstCard = null;
            SecondCard = null;
            isFirstCardAsigned = false;
        }

        private void EndProcessing()
        {
            _isProcessingPair = false; 
            _inputService.InputAvaliable = true;
            CustomDebug.Log("Обработка пары завершена, ввод разблокирован.");
        }

        private void CreateFlipEffect(GameEntity card)
        {
            _effectFactory.CreateEffect(
                _effectStaticDataService.GetEffectConfig(EffectTypeId.FlipEffect),
                card.Id, card.Id);
        }

        public bool Compare()
        {
            return FirstCard.CardTypeId == SecondCard.CardTypeId;
        }
    }
}