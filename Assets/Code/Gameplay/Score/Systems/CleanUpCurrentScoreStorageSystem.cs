using Entitas;

namespace Code.Gameplay.Score.Systems
{
    public class CleanUpCurrentScoreStorageSystem: ITearDownSystem
    {
        private readonly IGroup<MetaEntity> _scoreStorages;

        public CleanUpCurrentScoreStorageSystem(MetaContext contextParameter)
        {
            _scoreStorages = contextParameter.GetGroup(
                MetaMatcher.Storage
                );            

        }
        
        public void TearDown()
        {
            foreach (MetaEntity storage in _scoreStorages)
            {
                //storage.ReplaceCurrentScore(0);
            }
        }
    }
}