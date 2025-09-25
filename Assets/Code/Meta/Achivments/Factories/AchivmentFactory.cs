using Code.Common.Entity;
using Code.Common.Extensions;

namespace Code.Meta.Achivments.Factories
{
    public class AchivmentFactory : IAchivmentFactory
    {
        public MetaEntity CreateTapCounter()
        {
            return CreateMetaEntity.Empty()
                .AddTapCounted(0)
                .AddTapTargetCount(1)
                .With(e => e.isTapCounter = true)
                ;
        }

        public MetaEntity CreateGoldCounter()
        {
            return CreateMetaEntity.Empty()
                    .AddGoldTargetCount(1000)
                    .With(e => e.isGoldCounter = true)
                ;
        }
    }
}