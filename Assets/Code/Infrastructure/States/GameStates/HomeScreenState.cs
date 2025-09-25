using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Gameplay.Levels;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using Code.Meta;
using Code.Meta.UI.HUD.LoadingWindow;
using Code.Progress.SaveLoad;

namespace Code.Infrastructure.States.GameStates
{
  public class HomeScreenState : EndOfFrameExitState
  {
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private readonly IAudioFactory _audioFactory;
    private readonly ISaveLoadService _saveLoadService;
    private readonly ILevelDataProvider _levelDataProvider;
    private readonly LoadingController _loadingController;
    private HomeScreenFeature _homeScreenFeature;
    private AudioFeature _audioFeature;

    public HomeScreenState(
      ISystemFactory systems, 
      GameContext gameContext,
      IAudioFactory audioFactory, 
      ISaveLoadService saveLoadService, 
      ILevelDataProvider levelDataProvider)
    {
      _systems = systems;
      _gameContext = gameContext;
      _audioFactory = audioFactory;
      _saveLoadService = saveLoadService;
      _levelDataProvider = levelDataProvider;
    }
    
    public override void Enter()
    {
      _audioFactory.CreateMusic(MusicTypeId.HomeScreen);
      _homeScreenFeature = _systems.Create<HomeScreenFeature>();
      _homeScreenFeature.Initialize();
      _saveLoadService.SaveProgress();
    }


    protected override void OnUpdate()
    {
      _homeScreenFeature.Execute();
      _homeScreenFeature.Cleanup();
    }

    protected override void ExitOnEndOfFrame()
    {
      _homeScreenFeature.DeactivateReactiveSystems();
      _homeScreenFeature.ClearReactiveSystems();


      DestructEntities();
      
      _homeScreenFeature.Cleanup();
      _homeScreenFeature.TearDown();
      _homeScreenFeature = null;
    }
    
    private void DestructEntities()
    {
      foreach (GameEntity entity in _gameContext.GetEntities()) 
        entity.isDestructed = true;      
    }
  }
}