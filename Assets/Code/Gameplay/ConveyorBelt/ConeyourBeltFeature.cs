using Code.Gameplay.ConveyorBelt.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.ConveyorBelt
{
    public class ConeyourBeltFeature: Feature
    {
        public ConeyourBeltFeature(ISystemFactory systems)
        {
            Add(systems.Create<CreateBeltSystem>());
           // Add(systems.Create<UpdatePositionLerpSystem>());
                //    Add(systems.Create<UpdateCurrentDistanceSystem>());
            Add(systems.Create<UpdateCurrentLerpSystem>());
            Add(systems.Create<MarkReachedEndSystem>());
            Add(systems.Create<CollectReachedSystem>());
        }
    }
}