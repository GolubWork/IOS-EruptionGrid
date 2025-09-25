using Code.Gameplay;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.States.GameStates
{
    public class BattleLoopState : EndOfFrameExitState
    {
        private readonly ISystemFactory _systems;
        private readonly IBattleFeatureService _battleFeatureService;

        private readonly GameContext _gameContext;
        private BattleFeature _battleFeature;

        public BattleLoopState(GameContext gameContext, ISystemFactory systemFactory, IBattleFeatureService battleFeatureService)
        {

            _gameContext = gameContext;
            _systems = systemFactory;
            _battleFeatureService = battleFeatureService;
        }

        public override void Enter()
        {
            _battleFeatureService.Activate();
        }        

        protected override void OnUpdate()
        {
            UpdateBattleFeature();
        }

        private void UpdateBattleFeature()
        {
            _battleFeatureService.Execute();
        }        
        
        protected override void ExitOnEndOfFrame()
        {
         
        }        
    }
}