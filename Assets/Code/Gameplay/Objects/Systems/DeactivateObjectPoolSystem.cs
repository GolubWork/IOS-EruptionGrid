using Code.Gameplay.Objects.Factories;
using Entitas;

namespace Code.Gameplay.Objects.Systems
{
    public class DeactivateObjectPoolSystem: ITearDownSystem
    {
        private readonly IPhysicsObjectPool _objectPool;
        private readonly IDefaultObjectPool _defaultObjectPool;

        public DeactivateObjectPoolSystem(IPhysicsObjectPool objectPool, IDefaultObjectPool defaultObjectPool)
        {
            _objectPool = objectPool;
            _defaultObjectPool = defaultObjectPool;
        }

        public void TearDown()
        {
         //   _objectPool.CleanUp();
            _defaultObjectPool.CleanUp();
        }
    }
}