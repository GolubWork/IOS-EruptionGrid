using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;

namespace Code.Meta.UI.HUD.ShopWindow
{
    public class ShopWindowModel
    {
        protected readonly IStaticWindowService StaticWindowService;
        protected readonly IUpdatableWindowService UpdatableWindowService;
        protected readonly IAudioFactory AudioFactory;
        protected readonly IItemBar ItemBar;

        public ShopWindowModel(
            IStaticWindowService staticWindowService, 
            IUpdatableWindowService updatableWindowService,
            IAudioFactory audioFactory,
            IItemBar itemBar)
        {
            StaticWindowService = staticWindowService;
            UpdatableWindowService = updatableWindowService;
            AudioFactory = audioFactory;
            ItemBar = itemBar;
        }

        public void ReturnHome()
        {
            AudioFactory.CreateSound(SoundTypeId.BtnClick);
            StaticWindowService.Close(StaticWindowId.ShopWindow);
            UpdatableWindowService.Close(UpdatableWindowId.TotalCurrencyWindow);
            StaticWindowService.Open(StaticWindowId.HomeWindow);
        }


    }
}