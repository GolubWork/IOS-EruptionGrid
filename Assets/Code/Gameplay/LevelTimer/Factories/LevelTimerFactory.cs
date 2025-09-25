using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.LevelTimer.Factories
{
    public class LevelTimerFactory : ILevelTimerFactory
    {
        private readonly IIdentifierService _identifierService;

        public LevelTimerFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateLevelTimer()
        {
            return CreateGameEntity.Empty()
                .AddId(_identifierService.Next())
                .With(e => e.isLevelTimer = true);
        }
    }
}