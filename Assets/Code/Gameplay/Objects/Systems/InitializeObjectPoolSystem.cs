using Code.Gameplay.Objects.Factories;
using Entitas;

namespace Code.Gameplay.Objects.Systems
{
    public class InitializeObjectPoolSystem: IInitializeSystem
    {
        private readonly IPhysicsObjectPool _objectPool;
        private readonly IDefaultObjectPool _defaultObjectPool;

        public InitializeObjectPoolSystem(IPhysicsObjectPool objectPool, IDefaultObjectPool defaultObjectPool)
        {
            _objectPool = objectPool;
            _defaultObjectPool = defaultObjectPool;
        }

        public void Initialize()
        {
            _defaultObjectPool.Initialize();
          //  _objectPool.Initialize();
        }
    }
}