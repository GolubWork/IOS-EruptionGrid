using Code.Common.Extensions;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Objects.Factories
{
    public class PhysicsObjectPool : AObjectPool, IPhysicsObjectPool
    {
        protected override int ObjectsCount => 50; 
        
        public PhysicsObjectPool(IIdentifierService identifierService) : base(identifierService)
        {
        }

        public override void ReturnDefaultObject(GameEntity obj)
        {
            obj.Rigidbody2D.bodyType = UnityEngine.RigidbodyType2D.Kinematic;
            obj.isPhysicsBody = false;
            base.ReturnDefaultObject(obj);
        }

        protected override GameEntity BuildDefaultObject(GameEntity obj)
        {
            obj
                .AddId(_identifierService.Next())
                .AddViewPath(PrefabsDirectoryConstants.DefaultPhysicsObjectPrefabPath)
                .AddWorldPosition(Far)
                .With(e => e.isObjectFromPool = true)
                ;
            return obj;
        }

        protected override bool IsDefaultComponent(int index)
        {
            return index == GameComponentsLookup.Transform
                   || index == GameComponentsLookup.View
                   || index == GameComponentsLookup.Id
                   || index == GameComponentsLookup.WorldPosition
                   || index == GameComponentsLookup.Rigidbody2D
                   || index == GameComponentsLookup.SpriteRenderer
                ;
        }
    }
}