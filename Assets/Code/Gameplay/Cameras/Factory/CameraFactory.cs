using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Cameras.Factory
{
    public class CameraFactory : ICameraFactory
    {
        private readonly IIdentifierService _identifierService;

        public CameraFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateCamera(Vector3 at)
        {
            return CreateGameEntity.Empty()
                .AddId(_identifierService.Next())
                .AddWorldPosition(at)
                .AddViewPath(PrefabsDirectoryConstants.MainCameraPrefabPath)
                .With(e => e.isActive = false)
                .With(e => e.isMainCamera = true);
        }        
        
        public GameEntity CreateBorderCamera(Vector3 at)
        {
            return CreateGameEntity.Empty()
                .AddId(_identifierService.Next())
                .AddWorldPosition(at)
                .AddViewPath(PrefabsDirectoryConstants.BorderCameraPrefabPath)
                .With(e => e.isActive = false);
        }
        
    }
}