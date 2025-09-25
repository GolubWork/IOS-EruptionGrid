namespace Code.Meta.UI.HUD.ShopWindow.Providers
{
    public interface IShopProvider<T>
    {
        public T GetController();
        public void SetController(T controller);
    }
}