using Code.Gameplay.GameLoop;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.HUD.LoadingWindow;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadingHomeScreenState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly LoadingController _loadingController;
    private readonly IBattleFeatureService _battleFeatureService;

    public LoadingHomeScreenState(
      IGameStateMachine stateMachine, 
      ISceneLoader sceneLoader, 
      LoadingController loadingController,
      IBattleFeatureService battleFeatureService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingController = loadingController;
      _battleFeatureService = battleFeatureService;
    }
    
    public override void Enter()
    {
      _battleFeatureService.Deactivate();
      _sceneLoader.LoadSceneAddressable(ScenesDirectoryConstants.HomeScreenPath, EnterHomeScreenState);
    }

    private void EnterHomeScreenState()
    {
      _loadingController.Hide();
      _stateMachine.Enter<HomeScreenState>();
    }
  }
}