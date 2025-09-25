namespace Code.Meta.UI.HUD.HPContainer.Services
{
    public interface IHPBarService
    {
        HPBarController SetHPBar(HPBarController hpBarController);
        HPBarController GetHPBar();
    }
}