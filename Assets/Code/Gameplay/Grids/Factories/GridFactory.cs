using _Scripts.GridSpawn;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Objects.Factories;
using Code.Infrastructure;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Grids.Factories
{
    public class GridFactory : IGridFactory
    {
        private readonly IIdentifierService _identifierService;

        public GridFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateReferenceGrid(GridRows gridRows)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddGridRows(gridRows)
                    .With(e => e.isGrid = true)
                    .With(e => e.isReferenceGrid = true)
                ;
        }

        public GameEntity CreateReferenceGridRequest(GridRows gridRows)
        {
            return CreateGameEntity.Empty()
                    .AddGridRows(gridRows)
                    .With(e => e.isReferenceGridBuildRequest = true)
                ;
        }

        public GameEntity CreateMirrorGridRequest(GridRows gridRows)
        {
            return CreateGameEntity.Empty()
                    .AddGridRows(gridRows)
                    .With(e => e.isPlayerGridBuildRequest = true)
                ;
        }

        public GameEntity CreateMirrorGrid(GridRows gridRows)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddGridRows(gridRows)
                    .With(e => e.isGrid = true)
                    .With(e => e.isPlayerGrid = true)
                ;
        }
    }
}