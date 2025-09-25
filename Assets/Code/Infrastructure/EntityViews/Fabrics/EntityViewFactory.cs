using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.EntityViews.Adapter.Adapters;
using Code.Infrastructure.EntityViews.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.EntityViews.Fabrics
{
    public class EntityViewFactory<TBehaviour, TEntity> : IEntityViewFactory<TBehaviour, TEntity>
    where TBehaviour : Component, IEntityBehaviour<TEntity>
    where TEntity : Entitas.Entity
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        private readonly Vector3 _farAway = new Vector3(-999, -999, 0);

        public EntityViewFactory(
            IAssetProvider assetProvider, 
            IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public async UniTask<TBehaviour> CreateViewForEntityFromAddresable(IEntityViewAdapter<TBehaviour, TEntity > adapter, TEntity entity)
        {
            Object viewPrefab = await _assetProvider.LoadAsset<TBehaviour>(adapter.ViewPath);
            TBehaviour view = _instantiator.InstantiatePrefabForComponent<TBehaviour>(
                viewPrefab,
                position: _farAway,
                Quaternion.identity, 
                parentTransform: null
            );
            view.SetEntity(entity);
            return view;
        }

        
        public TBehaviour CreateFromPrefab(IEntityViewAdapter<TBehaviour, TEntity> adapter, TEntity entity)
        {
            TBehaviour view = _instantiator.InstantiatePrefabForComponent<TBehaviour>(
                adapter.ViewPrefab,
                position: _farAway,
                Quaternion.identity, 
                parentTransform: null
            );
            view.SetEntity(entity);
            return view;
        }
    }
}