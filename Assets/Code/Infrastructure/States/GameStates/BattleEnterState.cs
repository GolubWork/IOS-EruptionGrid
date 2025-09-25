using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Gameplay;
using Code.Gameplay.Backgrounds.Factory;
using Code.Gameplay.Cameras;
using Code.Gameplay.Cameras.Factory;
using Code.Gameplay.Chicken.Factory;
using Code.Gameplay.Eggs.Factories;
using Code.Gameplay.Floors.Factories;
using Code.Gameplay.GameLoop;
using Code.Gameplay.Levels;
using Code.Gameplay.LevelTimer.Factories;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class BattleEnterState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ILevelDataProvider _levelDataProvider;
    private readonly IChickenFactory _chickenFactory;
    private readonly ICameraFactory _cameraFactory;
    private readonly IFloorFactory _floorFactory;
    private readonly IBackgroundFactory _backgroundFactory;
    private readonly IEggFactory _eggFactory;
    private readonly ILevelTimerFactory _levelTimerFactory;
    private readonly IAudioFactory _audioFactory;
    private readonly InputContext _input;
    private readonly ICameraProvider _cameraProvider;
    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;
    private BattleFeature _battleFeature;

    public BattleEnterState(
      IGameStateMachine stateMachine, 
      ILevelDataProvider levelDataProvider, 
      IChickenFactory chickenFactory,
      ICameraFactory cameraFactory,
      IFloorFactory floorFactory,
      IBackgroundFactory backgroundFactory,
      IEggFactory eggFactory,
      ILevelTimerFactory levelTimerFactory,
      IAudioFactory audioFactory,
      InputContext input)
    {
      _stateMachine = stateMachine;
      _levelDataProvider = levelDataProvider;
      _chickenFactory = chickenFactory;
      _cameraFactory = cameraFactory;
      _floorFactory = floorFactory;
      _backgroundFactory = backgroundFactory;
      _eggFactory = eggFactory;
      _levelTimerFactory = levelTimerFactory;
      _audioFactory = audioFactory;
      _input = input;
    }
    
    public override void Enter()
    {
      var inputs = _input.GetGroup(InputMatcher.Input).GetSingleEntity();
      inputs.isInputAvaliable = true;
      
      _audioFactory.CreateMusic(MusicTypeId.GameTheme);
      PlaceCamera();
      PlaceBackground();
      PlaceTimer();
     
      _stateMachine.Enter<BattleLoopState>();
    }
    
    private void PlaceTimer()
    {
      _levelTimerFactory.CreateLevelTimer();
    }
    
    private void PlaceBackground()
    {
      _backgroundFactory.CreateBackground();
    }
    private void PlaceCamera()
    {
       _cameraFactory.CreateCamera(_levelDataProvider.StartPoint);
    }
    
  }
}