using Code.Gameplay.GameLoop;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Progress.SaveLoad;
using Code.Windows.StaticWindows;

namespace Code.Infrastructure.States.GameStates
{
  public class GameOverState : EndOfFrameExitState
  {
    private readonly IStaticWindowService _staticWindowService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly InputContext _input;
    private readonly GameContext _gameContext;

    public GameOverState(
      IStaticWindowService staticWindowService,
      ISaveLoadService saveLoadService,
      InputContext input)
    {
      _staticWindowService = staticWindowService;
      _saveLoadService = saveLoadService;
      _input = input;
    }

    public override void Enter()
    {
      var inputs = _input.GetGroup(InputMatcher.Input).GetSingleEntity();
      inputs.isInputAvaliable = false;
      _saveLoadService.SaveProgress();
      _staticWindowService.Open(StaticWindowId.GameOverWindow);
    }

    protected override void ExitOnEndOfFrame()
    {
        
    }
  }
}