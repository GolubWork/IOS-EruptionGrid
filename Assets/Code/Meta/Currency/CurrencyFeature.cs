using Code.Infrastructure.Systems;
using Code.Meta.Currency.Systems;

namespace Code.Meta.Currency
{
    public class CurrencyFeature: Feature
    {
        public CurrencyFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetCurrentCurrencyToUISystem>());
            Add(systems.Create<SetTotalCurrencyToUISystem>());
            Add(systems.Create<TearDownCurrencySystem>());
        }
    }
}