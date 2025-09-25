using Code.Common.Helpers;

namespace Code.Meta.UI.HUD.CurrencyContainer
{
    public class GoldBarModel
    {
        public string SetGold(float value)
        {
            string goldText = value.ToString("");
            return StringUpdater.UpdateString(goldText);
        }
    }
}