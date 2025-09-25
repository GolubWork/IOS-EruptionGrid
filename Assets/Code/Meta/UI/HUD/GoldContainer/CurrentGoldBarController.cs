
using Code.Meta.UI.HUD.CurrencyContainer.Services;
using Code.Meta.UI.HUD.GoldContainer.Services;
using Code.Windows.UpdatableWindows;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD.CurrencyContainer
{
    public class CurrentGoldBarController: UpdatableWindow
    {
        [SerializeField] private TextMeshProUGUI goldText;
        private GoldBarModel _model;
        private ICurrencyBarService _currencyBarService;
        private IUpdatableWindowService _staticWindowService;

        [Inject]
        private void Construct(
            IUpdatableWindowService staticWindowService, 
            ICurrencyBarService currencyBarService)
        {
            Id = UpdatableWindowId.CurrentCurrencyWindow;
            _model = new GoldBarModel();
            _currencyBarService = currencyBarService;
            _staticWindowService = staticWindowService;
        }
        protected override void Initialize()
        {
            _currencyBarService.SetCurrentCurrencyBar(this);
        }
        public void SetCurrentGold(float value)
        {
            goldText.text = _model.SetGold(value);
        }
        
        protected override void Cleanup()
        {
            _currencyBarService.SetCurrentCurrencyBar(null);
            _staticWindowService.Close(Id);
        }
    }
}