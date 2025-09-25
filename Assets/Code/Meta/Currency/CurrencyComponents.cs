using Code.Progress;
using Entitas;

namespace Code.Meta.Currency
{
    [Meta] public class CurrencyStorage : ISavedComponent { public float Value; }
    [Meta] public class SessionCurrency : ISavedComponent { public float Value; }
    [Meta] public class Currency : IComponent { }
    [Meta] public class AmountCurrency : IComponent { public int Value; }
}