using System.Collections.Generic;
using Code.Infrastructure.EntityViews.Adapter.Adapters.GameEntityViewAdapters;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Infrastructure.EntityViews.Fabrics;
using Entitas;

namespace Code.Infrastructure.EntityViews.Systems.GameEntityViewSystems
{
    public class BindGameEntityViewFromPrefabSystem : IExecuteSystem
    {
        private readonly IEntityViewFactory<GameEntityBehaviour, GameEntity> _iEntityViewFactory;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(32);

        public BindGameEntityViewFromPrefabSystem(GameContext contextParameter, IEntityViewFactory<GameEntityBehaviour, GameEntity> iEntityViewFactory)
        {
            _iEntityViewFactory = iEntityViewFactory;
            _entities = contextParameter.GetGroup(GameMatcher
                .AllOf(GameMatcher.ViewPrefab)
                .NoneOf(GameMatcher.View)
            );
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                _iEntityViewFactory.CreateFromPrefab(new GameEntityViewAdapter(entity),entity);
            }
        }
    }
}