using System.Collections;
using Code.Common.Extensions;
using Code.Gameplay.Objects.Factories;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Gameplay.Shelfs.Factories
{
    public class ShelfFactory : IShelfFactory
    {
        private readonly IDefaultObjectPool _defaultObjectPool;
        private readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        private readonly ICoroutineRunner _coroutineRunner;

        public ShelfFactory(IDefaultObjectPool defaultObjectPool, IAdditionalSpriteProvider additionalSpriteProvider, ICoroutineRunner coroutineRunner)
        {
            _defaultObjectPool = defaultObjectPool;
            _additionalSpriteProvider = additionalSpriteProvider;
            _coroutineRunner = coroutineRunner;
        }

        public GameEntity GetShelf(Vector3 at)
        {
            GameEntity defaultObject = _defaultObjectPool.ReserveDefaultObject();
            defaultObject
                .With(e => e.isShelf = true)
                ;
            
            defaultObject.ReplaceWorldPosition(at + new Vector3(0.3f, -0.3f, -1));
            
            _coroutineRunner.StartCoroutine(
                WaitForSpriteRenderer(defaultObject, () =>
                {
                    defaultObject.SpriteRenderer.sprite = _additionalSpriteProvider.GetConfig().Shelf;
                }));
            
            return defaultObject;
        }
        private IEnumerator WaitForSpriteRenderer(GameEntity entity, System.Action callback)
        {
            while (entity.hasSpriteRenderer == false)
            {
                yield return null;
            }
            callback?.Invoke();
        }



        public void ReturnShelf(GameEntity shelf)
        {
            _defaultObjectPool.ReturnDefaultObject(shelf);
        }
    }
}