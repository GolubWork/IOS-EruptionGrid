using System.Collections.Generic;
using Code.Gameplay.Grids.Factories;
using Entitas;

namespace Code.Gameplay.Grids.Systems
{
    public class ProcessGridBuildRequestSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGridFactory _gridFactory;

        public ProcessGridBuildRequestSystem(Contexts contexts, IGridFactory gridFactory)
            : base(contexts.game)
        {
            _gridFactory = gridFactory;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ReferenceGridBuildRequest.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isReferenceGridBuildRequest && entity.hasGridRows;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                _gridFactory.CreateReferenceGrid(e.gridRows.Value);
                e.Destroy();
            }
        }
    }
}