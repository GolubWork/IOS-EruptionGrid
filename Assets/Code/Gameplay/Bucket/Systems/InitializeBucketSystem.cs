using Code.Gameplay.Bucket.Factories;
using Entitas;

namespace Code.Gameplay.Bucket.Systems
{
    public class InitializeBucketSystem: IInitializeSystem
    {
        private readonly IBucketFactory _bucketFactory;

        public InitializeBucketSystem(IBucketFactory bucketFactory)
        {
            _bucketFactory = bucketFactory;
        }

        public void Initialize()
        {
            _bucketFactory.GetBucket();
        }
    }
}

