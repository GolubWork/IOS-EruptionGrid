using Entitas;

namespace Code.Gameplay.Score.Systems
{
    public class SetBestScoreSystem: IExecuteSystem
    {
        private readonly IGroup<MetaEntity> _scoreStorages;

        public SetBestScoreSystem(MetaContext meta)
        {
            _scoreStorages = meta.GetGroup(MetaMatcher.AllOf(
                MetaMatcher.Storage,
                MetaMatcher.SessionScore,
                MetaMatcher.BestScore
            ));
        }

        public void Execute()
        {
            foreach (MetaEntity storage in _scoreStorages)
            {
                if (storage.SessionScore >= storage.BestScore)
                {
                    storage.ReplaceBestScore(storage.SessionScore);
                }
            }
        }
    }
}