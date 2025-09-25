using Code.Meta.UI.HUD.CurrencyContainer.Services;
using Code.Windows.UpdatableWindows;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Meta.UI.HUD.CurrencyContainer
{
    public class TotalCurrencyBarController: UpdatableWindow
    {
        [SerializeField] private TextMeshProUGUI currencyText;
        private CurrencyBarModel _model;
        private ICurrencyService _currencyService;
        private IUpdatableWindowService _staticWindowService;

        [Inject]
        private void Construct(
            IUpdatableWindowService staticWindowService, 
            ICurrencyService currencyService)
        {
            Id = UpdatableWindowId.TotalCurrencyWindow;
            _model = new CurrencyBarModel();
            _currencyService = currencyService;
            _staticWindowService = staticWindowService;
        }
        protected override void Initialize()
        {
            _currencyService.SetTotalCurrencyBar(this);
        }
        public void SetStoragedCurrency(float value)
        {
            currencyText.text = _model.SetGold(value);
        }
        protected override void Cleanup()
        {
            _currencyService.SetTotalCurrencyBar(null);
            _staticWindowService.Close(Id);
        }
    }
}