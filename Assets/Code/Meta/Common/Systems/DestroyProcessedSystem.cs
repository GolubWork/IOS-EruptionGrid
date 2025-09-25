using System.Collections.Generic;
using Entitas;

namespace Code.Meta.Common.Systems
{
    public class DestroyProcessedSystem: IExecuteSystem
    {
        private readonly IGroup<MetaEntity> _processed;
        private List<MetaEntity> _buffer = new (1);

        public DestroyProcessedSystem(MetaContext meta)
        {
            _processed = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Processed
            ));
        }

        public void Execute()
        {
            foreach (MetaEntity entity in _processed.GetEntities(_buffer))
            {
                entity.Destroy();
            }
        }
    }
}