using Code.Gameplay.Physics.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Physics
{
    public class PhysicsFeature : Feature
    {
        public PhysicsFeature(ISystemFactory systems)
        {
            Add(systems.Create<SyncInitialRigidbodyPositionSystem>());
            
            Add(systems.Create<CreateHeroForceOnSpacePressed>());
            
            Add(systems.Create<ApplyForceSystem>());
            
            Add(systems.Create<UpdateWorldPositionByRigidbody2DPosition>());

            Add(systems.Create<FinalizeProcessedForcesSystem>());
        }
    }
}