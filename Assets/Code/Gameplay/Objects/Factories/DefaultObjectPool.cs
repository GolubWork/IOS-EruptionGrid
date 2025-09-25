using Code.Common.Extensions;
using Code.Infrastructure.AssetManagement.Constants;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Objects.Factories
{
    public class DefaultObjectPool : AObjectPool, IDefaultObjectPool
    {
        public DefaultObjectPool(IIdentifierService identifierService) : base(identifierService)
        {
        }
        

        protected override bool IsDefaultComponent(int index)
        {
            return index == GameComponentsLookup.Transform
                   || index == GameComponentsLookup.View
                   || index == GameComponentsLookup.Id
                   || index == GameComponentsLookup.WorldPosition
                   || index == GameComponentsLookup.SpriteRenderer
                ;
        }
    }
}