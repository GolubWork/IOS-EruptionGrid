using Code.Meta.UI.HUD.GoldContainer.Services;

namespace Code.Meta.UI.HUD.CurrencyContainer.Services
{
    public class CurrencyBarService: ICurrencyBarService
    {
        private CurrentGoldBarController _currentGoldBarController;
        public CurrentGoldBarController SetCurrentCurrencyBar(CurrentGoldBarController currentScoreBarController)
        {
            _currentGoldBarController = currentScoreBarController;
            return _currentGoldBarController;
        }
        public CurrentGoldBarController GetCurrentCurrencyBar()
        {
            return _currentGoldBarController;
        }

        private TotalGoldBarController _totalGoldBarController;

        public TotalGoldBarController SetTotalCurrencyBar(TotalGoldBarController currentScoreBarController)
        {
            _totalGoldBarController = currentScoreBarController;
            return _totalGoldBarController;
        }

        public TotalGoldBarController GetTotalCurrencyBar()
        {
            return _totalGoldBarController;
        }
    }
}