using System.Linq;
using Code.Input.Service;
using Code.Meta.Achivments.Configs;
using Entitas;

namespace Code.Meta.Achivments.Systems
{
    public class ProcessTapCounterSystem: IExecuteSystem
    {
        private readonly ITouchInputService _inputService;
        private readonly IGroup<MetaEntity> _tapCounters;
        private readonly IGroup<MetaEntity> _achivments;

        public ProcessTapCounterSystem(MetaContext meta, ITouchInputService inputService)
        {
            _inputService = inputService;
            _tapCounters = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.TapCounter,
                MetaMatcher.TapTargetCount
            ));

            _achivments = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.AchivmentsStorage
            ));
        }

        public void Execute()
        {
            if(_tapCounters.GetEntities().Length == 0) return;
            MetaEntity tapCounter = _tapCounters.GetEntities().FirstOrDefault();
            if(tapCounter == null) return;
            if (_inputService.GetLeftMouseButtonDown())
            {
                tapCounter.ReplaceTapCounted(tapCounter.TapCounted + 1);
            }
            if (tapCounter.TapCounted >= tapCounter.TapTargetCount)
            {
                foreach (AchivmentData data in _achivments.GetEntities().FirstOrDefault()!.AchivmentsStorage)
                {
                    if (data.AchivmentTypeId == AchivmentTypeId.TapedTimes)
                    {
                        data.achivmentStatusId = AchivmentStatusId.Completed;
                    }
                }
            }
        }
    }
}