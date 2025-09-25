using Code.Meta.UI.HUD.CurrencyContainer;
using Code.Meta.UI.HUD.CurrencyContainer.Services;
using Entitas;

namespace Code.Meta.Currency.Systems
{
    public class SetTotalCurrencyToUISystem: IExecuteSystem
    {
        private readonly ICurrencyService _currencyService;
        private readonly IGroup<MetaEntity> _coinsStorage;
        
        public SetTotalCurrencyToUISystem(MetaContext meta, ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _coinsStorage = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage, 
                MetaMatcher.SessionCurrency));
        }

        public void Execute()
        {
            foreach (MetaEntity storage in _coinsStorage)
            {
                TotalCurrencyBarController controller = _currencyService.GetTotalCurrencyBar();
                if(!controller) continue;
                controller.SetStoragedCurrency(storage.CurrencyStorage);
            }
        }
    }
}