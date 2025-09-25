using Code.Gameplay.GameResource.Factories;
using Entitas;

namespace Code.Gameplay.GameResource.Systems
{
    public class InitializeGameResourceSystem: IInitializeSystem
    {
        private readonly IGameResourceFactory _gameResourceFactory;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<MetaEntity> _levels;

        public InitializeGameResourceSystem(
            MetaContext meta,
            IGameResourceFactory gameResourceFactory)
        {
            _gameResourceFactory = gameResourceFactory;
            _levels = meta.GetGroup(MetaMatcher.ChosenLevel);
        }
        
        public void Initialize()
        {
            foreach (MetaEntity level in _levels)
            {
                int gameResources = level.ChosenLevel.gameResource = level.ChosenLevel.gameResource;
                _gameResourceFactory.Create(gameResources);
            }
        }
    }
}