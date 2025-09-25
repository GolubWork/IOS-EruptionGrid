using Code.Gameplay.Chicken.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Chicken
{
    public class ChickenFeature: Feature
    {
        public ChickenFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeChickenSystem>());
            
            Add(systems.Create<SetChickenDirectionByInputSystem>());

            Add(systems.Create<ChickenDeathSystem>());
            
            Add(systems.Create<FinalizeChickenDeathProcessSystem>());
        }
    }
}