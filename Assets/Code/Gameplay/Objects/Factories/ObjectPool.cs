using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Objects.Factories
{
    public class ObjectPool: AObjectPool
    {
        public ObjectPool(IIdentifierService identifierService) : base(identifierService)
        {
        }
    }
}