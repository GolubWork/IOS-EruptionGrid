using Code.Infrastructure.States.StateInfrastructure;
using Code.Windows.StaticWindows;
using Code.Windows.UpdatableWindows;

namespace Code.Infrastructure.States.GameStates
{
    public class GamePauseState:  SimpleState
    {
        private readonly IStaticWindowService _staticWindowService;
        private readonly IUpdatableWindowService _updatableWindowService;

        public GamePauseState(
            IStaticWindowService staticWindowService, IUpdatableWindowService updatableWindowService)
        {
            _staticWindowService = staticWindowService;
            _updatableWindowService = updatableWindowService;
        }
    
        public override void Enter()
        {
            _staticWindowService.Open(StaticWindowId.PauseWindow);
        }
    }
}