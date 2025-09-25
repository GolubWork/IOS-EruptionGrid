using System;
using Code.Gameplay.StaticData.WindowsStaticData;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Meta.UI.HUD.LoadingWindow;

namespace Code.Infrastructure.States.GameStates
{
  public class BootstrapState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly LoadingController _loadingController;
    private readonly IWindowsStaticDataService _staticWindowService;

    public BootstrapState(
      IGameStateMachine stateMachine, 
      ISceneLoader sceneLoader, 
      LoadingController loadingController, 
      IWindowsStaticDataService staticWindowService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingController = loadingController;
      _staticWindowService = staticWindowService;
    }
    
    public override async void Enter()
    {
      try
      {
        await _staticWindowService.LoadAll();
        _sceneLoader.LoadSceneBuildIn(ScenesDirectoryConstants.LoadingScenePath, EnterLoadingState);
      }
      catch (Exception e)
      {
        throw new Exception($"Cannot load _staticWindowService: {e}");
      }
    }
    private void EnterLoadingState()
    {
      _loadingController.Show();
      _stateMachine.Enter<LoadResourcesState>();
    }
  }
}