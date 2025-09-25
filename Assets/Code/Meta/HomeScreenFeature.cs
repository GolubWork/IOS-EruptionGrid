using Code.Audios.Audio;
using Code.Common.Destruct;
using Code.Infrastructure.EntityViews;
using Code.Infrastructure.Systems;
using Code.Input;
using Code.Meta.Common.Systems;
using Code.Meta.Currency;
using Code.Meta.Shop;
using Code.Meta.Skins;

namespace Code.Meta
{
  public class HomeScreenFeature : Feature
  {
    public HomeScreenFeature(ISystemFactory systems)
    {
      Add(systems.Create<BindViewFeature>());
      Add(systems.Create<InputFeature>());
      Add(systems.Create<AudioFeature>());
      Add(systems.Create<ShopFeature>());
      Add(systems.Create<SkinFeature>());
      Add(systems.Create<ProcessDestructedFeature>());
      Add(systems.Create<CurrencyFeature>());
      
      Add(systems.Create<DestroyProcessedSystem>());
    }
  }
}