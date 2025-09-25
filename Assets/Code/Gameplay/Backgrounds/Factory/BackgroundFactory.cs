using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Backgrounds.Factory
{
    public class BackgroundFactory : IBackgroundFactory
    {
        private readonly IIdentifierService _identifierService;

        public BackgroundFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }


        public GameEntity CreateBackground()
        {
            return CreateGameEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddViewPath(PrefabsDirectoryConstants.BacgroundPrefabPath)
                    .With(e => e.isBackground = true)
                ;
        }
    }
}