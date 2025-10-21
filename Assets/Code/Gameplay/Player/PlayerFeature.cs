using Code.Gameplay.Player.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Player
{
    public class PlayerFeature: Feature
    {
        public PlayerFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializePlayerSystem>());
        }
    }
}