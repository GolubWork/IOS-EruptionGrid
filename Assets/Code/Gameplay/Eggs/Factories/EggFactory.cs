using Code.Common.Extensions;
using Code.Gameplay.Effects;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.Objects.Factories;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.RandomSpriteStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using UnityEngine;

namespace Code.Gameplay.Eggs.Factories
{
    public class EggFactory : IEggFactory
    {
        private readonly IPhysicsObjectPool _objectPool;
        private readonly IRandomSpriteProvider _randomSpriteProvider;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;

        public EggFactory(IPhysicsObjectPool objectPool, IRandomSpriteProvider randomSpriteProvider, 
            IEffectStaticDataService effectStaticDataService,
            IVisualEffectStaticDataService visualEffectStaticDataService)
        {
            _objectPool = objectPool;
            _randomSpriteProvider = randomSpriteProvider;
            _effectStaticDataService = effectStaticDataService;
            _visualEffectStaticDataService = visualEffectStaticDataService;
        }

        public GameEntity GetEgg(Vector3 at)
        {
            GameEntity defaultObject = _objectPool.ReserveDefaultObject();
            defaultObject
                .AddTotalTapsRequired(3)
                .AddTapsRequired(3)
                .AddTapRapeatableTimes(3)
                .AddTapEffectConfig(_effectStaticDataService.GetEffectConfig(EffectTypeId.Tap))
                .AddTapVisualEffectConfig(_visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.TapEffect))
                .With(e => e.isTapable = true)
                
                .With(e => e.isEgg = true)
                .With(e => e.isPhysicsBody = true);

            defaultObject.ReplaceWorldPosition(at + new Vector3(0.3f, -0.3f, -1));
            defaultObject.SpriteRenderer.sprite = _randomSpriteProvider.GetRandomSprite();
            defaultObject.Rigidbody2D.position = at + new Vector3(0.3f, -0.3f, -1);
            defaultObject.Rigidbody2D.linearVelocity = new Vector2(Random.Range(-1,1f),0);
            defaultObject.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            return defaultObject;
        }

        public void ReturnEgg(GameEntity egg)
        {
            _objectPool.ReturnDefaultObject(egg);
        }
    }
}