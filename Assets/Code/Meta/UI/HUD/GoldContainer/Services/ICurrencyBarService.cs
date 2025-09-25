using Code.Meta.UI.HUD.CurrencyContainer;

namespace Code.Meta.UI.HUD.GoldContainer.Services
{
    public interface ICurrencyBarService
    {
        CurrentGoldBarController SetCurrentCurrencyBar(CurrentGoldBarController currentScoreBarController);
        CurrentGoldBarController GetCurrentCurrencyBar();
        
        TotalGoldBarController SetTotalCurrencyBar(TotalGoldBarController currentScoreBarController);
        TotalGoldBarController GetTotalCurrencyBar();
    }
}