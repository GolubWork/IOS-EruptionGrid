namespace Code.Meta.UI.HUD.CurrencyContainer.Services
{
    public interface ICurrencyService
    {
        CurrentCurrencyController SetCurrentCurrencyBar(CurrentCurrencyController currentScoreBarController);
        CurrentCurrencyController GetCurrentCurrencyBar();
        
        TotalCurrencyBarController SetTotalCurrencyBar(TotalCurrencyBarController currentScoreBarController);
        TotalCurrencyBarController GetTotalCurrencyBar();
    }
}