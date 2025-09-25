using Code.Common.Helpers;

namespace Code.Meta.UI.HUD.CurrencyContainer
{
    public class CurrencyBarModel
    {
        public string SetGold(float value)
        {
            string currencyText = value.ToString("");
            return StringUpdater.UpdateString(currencyText);
        }
    }
}