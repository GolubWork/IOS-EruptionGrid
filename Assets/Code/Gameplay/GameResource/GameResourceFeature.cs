using Code.Gameplay.GameResource.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.GameResource
{
    public class GameResourceFeature: Feature
    {
        public GameResourceFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeGameResourceSystem>());
            Add(systems.Create<SetToUIGameResoruceSystem>());
        }
    }
}