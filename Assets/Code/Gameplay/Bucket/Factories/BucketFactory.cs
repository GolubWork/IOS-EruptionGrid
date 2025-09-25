using System.Collections;
using Code.Gameplay.Objects.Factories;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Gameplay.Bucket.Factories
{
    public class BucketFactory : IBucketFactory
    {
        private readonly IDefaultObjectPool _defaultObjectPool;
        private readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        private readonly ICoroutineRunner _coroutineRunner;

        public BucketFactory(
            IDefaultObjectPool defaultObjectPool, 
            IAdditionalSpriteProvider additionalSpriteProvider,
            ICoroutineRunner coroutineRunner)
        {
            _defaultObjectPool = defaultObjectPool;
            _additionalSpriteProvider = additionalSpriteProvider;
            _coroutineRunner = coroutineRunner;
        }

        public GameEntity GetBucket()
        {
            GameEntity defaultObject = _defaultObjectPool.ReserveDefaultObject();
            _coroutineRunner.StartCoroutine(SetBucket(defaultObject));
            return defaultObject;
        }

        private IEnumerator SetBucket(GameEntity defaultObject)
        {
            defaultObject.isBucket = true;
            while (!defaultObject.hasSpriteRenderer)
            {
                yield return null;
            }
            defaultObject.SpriteRenderer.sprite = _additionalSpriteProvider.GetConfig().Bucket;
            while (!defaultObject.hasWorldPosition)
            {
                yield return null;
            }
            defaultObject.ReplaceWorldPosition(new Vector3(0, -4.5f, 0));
            
            defaultObject.isFollowMouseX = true;            
            defaultObject.isGrabable = true;
            yield return null;
        }
    }
}