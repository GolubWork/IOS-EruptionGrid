using System.Collections.Generic;
using System.Linq;
using Code.Meta.Achivments.Configs;
using Entitas;

namespace Code.Meta.Achivments.Systems
{
    public class ProcessCurrencyCounterSystem: IExecuteSystem
    {
        private readonly IGroup<MetaEntity> _goldStorages;
        private readonly IGroup<MetaEntity> _goldCounters;
        private readonly IGroup<MetaEntity> _achivments;
        private List<MetaEntity> _buffer = new (1);

        public ProcessCurrencyCounterSystem(MetaContext meta)
        {
            _goldCounters = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.GoldCounter,
                MetaMatcher.GoldTargetCount
                ));

            _goldStorages = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.CurrencyStorage,
                MetaMatcher.Storage,
                MetaMatcher.SessionCurrency
            ));

            _achivments = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.AchivmentsStorage,
                MetaMatcher.Storage
            ));
        }

        public void Execute()
        {
            if(_goldStorages.GetEntities().Length == 0) return;
            if(_goldCounters.GetEntities().Length == 0) return;
            if (_goldStorages.GetEntities().FirstOrDefault()?.SessionCurrency >= 
                _goldCounters.GetEntities().FirstOrDefault()?.GoldTargetCount
               )
            {
                foreach (AchivmentData data in _achivments.GetEntities().FirstOrDefault()!.AchivmentsStorage)
                {
                    if (data.AchivmentTypeId == AchivmentTypeId.CollectedGold)
                    {
                        data.achivmentStatusId = AchivmentStatusId.Completed;
                    }
                }
            }
        }
    }
}