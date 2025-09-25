using Code.Meta.UI.HUD.ShopWindow.SkinShop;

namespace Code.Meta.UI.HUD.ShopWindow.Providers
{
    public class SkinShopProvider: IShopProvider<SkinShopWindowController>
    {
        private SkinShopWindowController _skinShopWindowController;
        
        public SkinShopWindowController GetController()
        => _skinShopWindowController;

        public void SetController(SkinShopWindowController controller)
        => _skinShopWindowController = controller;
    }


}