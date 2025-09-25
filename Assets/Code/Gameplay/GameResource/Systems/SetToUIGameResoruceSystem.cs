using Code.Meta.UI.HUD.ResourceWindow.Services;
using Entitas;

namespace Code.Gameplay.GameResource.Systems
{
    public class SetToUIGameResoruceSystem: IExecuteSystem
    {
        private readonly IGameResourceService _gameResourceService;
        private readonly IGroup<GameEntity> _gameResources;

        public SetToUIGameResoruceSystem(GameContext game, IGameResourceService gameResourceService)
        {
            _gameResourceService = gameResourceService;
            _gameResources = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.GameResource,
                GameMatcher.ResourceValue
            ));
        }

        public void Execute()
        {
            foreach (GameEntity resource in _gameResources)
            {
                _gameResourceService.GetGameResourceController().SetCurrentResource(resource.ResourceValue);
            }
        }
    }
}