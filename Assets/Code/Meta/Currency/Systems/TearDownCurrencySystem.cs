using Entitas;

namespace Code.Meta.Currency.Systems
{
    public class TearDownCurrencySystem: ITearDownSystem
    {
        private readonly IGroup<MetaEntity> _goldStorages;

        public TearDownCurrencySystem(MetaContext meta)
        {
            _goldStorages = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.CurrencyStorage,
                MetaMatcher.SessionCurrency,
                MetaMatcher.Storage
                ));
        }

        public void TearDown()
        {
            foreach (MetaEntity storage in _goldStorages)
            {
                storage.ReplaceCurrencyStorage(storage.CurrencyStorage + storage.SessionCurrency);
                storage.ReplaceSessionCurrency(0);
            }
        }
    }
}