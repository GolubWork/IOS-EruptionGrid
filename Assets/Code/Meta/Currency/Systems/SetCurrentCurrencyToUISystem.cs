using Code.Meta.UI.HUD.CurrencyContainer;
using Code.Meta.UI.HUD.CurrencyContainer.Services;
using Entitas;

namespace Code.Meta.Currency.Systems
{
    public class SetCurrentCurrencyToUISystem: IExecuteSystem
    {
        private readonly ICurrencyService _currencyService;
        private readonly IGroup<MetaEntity> _currencyStorage;
        
        public SetCurrentCurrencyToUISystem(MetaContext meta, ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _currencyStorage = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage, 
                MetaMatcher.SessionCurrency));
        }

        public void Execute()
        {
            foreach (MetaEntity storage in _currencyStorage)
            {
                CurrentCurrencyController controller = _currencyService.GetCurrentCurrencyBar();
                if(!controller) continue;
                controller.SetCurrentGold(storage.SessionCurrency);
            }
        }
    }
}