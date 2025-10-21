using System.Collections;
using Code.Common.Helpers;
using Code.Gameplay;
using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Meta.UI.HUD.LoadingWindow;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
    public class BattleLoopState : EndOfFrameExitState
    {
        private readonly IBattleFeatureService _battleFeatureService;
        private readonly LoadingController _loadingWindow;
        private readonly ICoroutineRunner _coroutineRunner;
        private BattleFeature _battleFeature;

        public BattleLoopState(IBattleFeatureService battleFeatureService, LoadingController loadingWindow, ICoroutineRunner coroutineRunner)
        {
            _battleFeatureService = battleFeatureService;
            _loadingWindow = loadingWindow;
            _coroutineRunner = coroutineRunner;
        }

        public override void Enter()
        {
            _coroutineRunner.StartCoroutine(StartGame());
            CustomDebug.Log("Enter BattleLoopState");
        }

        private IEnumerator StartGame()
        {
            _battleFeatureService.Activate();
            UpdateBattleFeature();
            yield return new WaitForSeconds(1);
            _loadingWindow.Hide();
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