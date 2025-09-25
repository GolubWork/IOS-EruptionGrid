namespace Code.Meta.UI.HUD.CurrencyContainer.Services
{
    public class CurrencyService: ICurrencyService
    {
        private CurrentCurrencyController _currentCurrencyController;
        public CurrentCurrencyController SetCurrentCurrencyBar(CurrentCurrencyController currentScoreBarController)
        {
            _currentCurrencyController = currentScoreBarController;
            return _currentCurrencyController;
        }
        public CurrentCurrencyController GetCurrentCurrencyBar()
        {
            return _currentCurrencyController;
        }

        private TotalCurrencyBarController _totalCurrencyBarController;

        public TotalCurrencyBarController SetTotalCurrencyBar(TotalCurrencyBarController currentScoreBarController)
        {
            _totalCurrencyBarController = currentScoreBarController;
            return _totalCurrencyBarController;
        }

        public TotalCurrencyBarController GetTotalCurrencyBar()
        {
            return _totalCurrencyBarController;
        }
    }
}