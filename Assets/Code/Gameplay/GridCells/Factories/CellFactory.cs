using System.Collections;
using Code.Common.Extensions;
using Code.Gameplay.Effects;
using Code.Gameplay.EffectsVisual.Configs;
using Code.Gameplay.Objects.Factories;
using Code.Gameplay.StaticData.AdditionalSpriteProvider;
using Code.Gameplay.StaticData.EffectStaticData;
using Code.Gameplay.StaticData.VisualEffectStaticData;
using Code.Infrastructure;
using UnityEngine;

namespace Code.Gameplay.GridCells.Factories
{
    public class CellFactory : ICellFactory
    {
        
        private readonly IDefaultObjectPool _defaultObjectPool;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IAdditionalSpriteProvider _additionalSpriteProvider;
        private readonly IEffectStaticDataService _effectStaticDataService;
        private readonly IVisualEffectStaticDataService _visualEffectStaticDataService;

        public CellFactory(
            IDefaultObjectPool defaultObjectPool, 
            ICoroutineRunner coroutineRunner, 
            IAdditionalSpriteProvider additionalSpriteProvider,
            IEffectStaticDataService effectStaticDataService,
            IVisualEffectStaticDataService visualEffectStaticDataService)
        {
            _defaultObjectPool = defaultObjectPool;
            _coroutineRunner = coroutineRunner;
            _additionalSpriteProvider = additionalSpriteProvider;
            _effectStaticDataService = effectStaticDataService;
            _visualEffectStaticDataService = visualEffectStaticDataService;
        }
        
        public GameEntity CreateGridCell(Vector3 at)
        {
            GameEntity cell = _defaultObjectPool.ReserveDefaultObject();
            cell.ReplaceWorldPosition(at)
                .With(e => e.isGridCell = true)
                ;
            return cell;
        }
        
        public GameEntity CreateCellFill(Vector3 at, int cellId)
        {
            GameEntity filler = _defaultObjectPool.ReserveDefaultObject();
            filler.ReplaceWorldPosition(at)
                .AddLinkedCellId(cellId)
                .With(e => e.isCellFiller = true)
                ;
            _coroutineRunner.StartCoroutine(SetFillerSprite(filler));
            return filler;
        }

        public void ReturnCellFill(GameEntity filler) => _defaultObjectPool.ReturnDefaultObject(filler);
        public GameEntity CreateMirrorGridCell(Vector3 vector3)
        {
            GameEntity cell = _defaultObjectPool.ReserveDefaultObject();
            cell.ReplaceWorldPosition(vector3)
                .With(e => e.isGridCell = true)
                
                
                .AddTotalTapsRequired(1)
                .AddTapsRequired(1)
                .AddTapRapeatableTimes(-1)
                .AddTapEffectConfig(_effectStaticDataService.GetEffectConfig(EffectTypeId.Tap))
                .AddTapVisualEffectConfig(_visualEffectStaticDataService.GetVisualEffectConfig(VisualEffectTypeId.TapEffect))
                .With(e => e.isTapable = true)
                
                .With(e => e.isRequireSkinApplication = true)
                ;
            return cell;
        }
        


        private IEnumerator SetFillerSprite(GameEntity filler)
        {
            while (!filler.hasSpriteRenderer)
                yield return null;
            filler.SpriteRenderer.sprite = _additionalSpriteProvider.GetConfig().CellFiller;
            while (!filler.hasBoxCollider)
                yield return null;
            filler.BoxCollider.enabled = false;
        }
    }
}