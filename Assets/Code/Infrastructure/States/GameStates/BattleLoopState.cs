using Code.Common.Helpers;
using Code.Gameplay;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;

namespace Code.Infrastructure.States.GameStates
{
    public class BattleLoopState : EndOfFrameExitState
    {
        private readonly IBattleFeatureService _battleFeatureService;
        private BattleFeature _battleFeature;

        public BattleLoopState(IBattleFeatureService battleFeatureService)
        {
            _battleFeatureService = battleFeatureService;
        }

        public override void Enter()
        {
            CustomDebug.Log("Enter BattleLoopState");
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