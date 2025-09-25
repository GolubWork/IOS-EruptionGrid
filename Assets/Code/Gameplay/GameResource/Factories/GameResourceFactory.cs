using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.GameResource.Factories
{
    public class GameResourceFactory : IGameResourceFactory
    {
        private readonly IIdentifierService _identifiers;
        
        public GameResourceFactory(
            IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }
        
        public GameEntity Create(int value)
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddResourceValue(value)
                    .With(e => e.isGameResource = true)
                ;
        }
    }
}