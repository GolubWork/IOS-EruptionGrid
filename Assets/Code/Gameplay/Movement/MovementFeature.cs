using Code.Gameplay.Movement.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Movement
{
    public class MovementFeature: Feature
    {
        public MovementFeature(ISystemFactory systems)
        {
            Add(systems.Create<DirectionalDeltaMoveSystem>());
            Add(systems.Create<ReturnOnInitialPositionSystem>());
            Add(systems.Create<SwitchPlayerDirectionBySwipeSystem>());
            Add(systems.Create<OneStepMovementSystem>());
            
            Add(systems.Create<TurnAlongDirectionSystem>());
            Add(systems.Create<RotateAlongDirectionSystem>());
            
            Add(systems.Create<UpdateTransformPositionSystem>());

        }
    }
}