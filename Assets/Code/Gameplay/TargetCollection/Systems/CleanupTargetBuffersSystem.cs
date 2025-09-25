using Entitas;

namespace Code.Gameplay.TargetCollection.Systems
{
    public class CleanupTargetBuffersSystem: ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CleanupTargetBuffersSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(
                GameMatcher.TargetsBuffer
            );
        }
        public void Cleanup()
        {
            foreach (GameEntity entity in _entities)
            {
                entity.TargetsBuffer.Clear();
            }
        }
    }
}