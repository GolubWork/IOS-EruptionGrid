using Code.Infrastructure.Systems;
using UnityEngine;

namespace Code.Gameplay.GameLoop
{
    public class BattleFeatureService : IBattleFeatureService
    {
        private BattleFeature _battleFeature;
        private readonly ISystemFactory _systems;
        private readonly GameContext _gameContext;


        public BattleFeatureService(ISystemFactory systems, GameContext gameContext)
        {
            _systems = systems;
            _gameContext = gameContext;
        }
        
        public void Activate()
        {
            if (_battleFeature != null) return;
            
            _battleFeature = _systems.Create<BattleFeature>(); 
            _battleFeature.Initialize();
        }

        public void Execute()
        {
            _battleFeature?.Execute();
            _battleFeature?.Cleanup();
        }

        public void Deactivate()
        {
            if(_battleFeature == null) return;
            _battleFeature.DeactivateReactiveSystems();
            _battleFeature.ClearReactiveSystems();

            DestructEntities();

            _battleFeature.Cleanup();
            _battleFeature.TearDown();
            _battleFeature = null;
        }

        
        private void DestructEntities()
        {
            foreach (GameEntity entity in _gameContext.GetEntities())
            {
                if (entity.isPersistent)
                {
                    Debug.Log($"Ignore persistent entity id: {entity.Id}");
                    continue;
                }
                entity.isDestructed = true;
            }
        }
        
    }
}