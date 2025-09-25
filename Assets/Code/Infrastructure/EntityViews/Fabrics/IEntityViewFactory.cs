using Code.Infrastructure.EntityViews.Adapter.Adapters;
using Code.Infrastructure.EntityViews.Behaviours;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.EntityViews.Fabrics
{
    public interface IEntityViewFactory<TBehaviour, TEntity>
        where TBehaviour : Component, IEntityBehaviour<TEntity>
        where TEntity : Entitas.Entity
    {
        UniTask<TBehaviour> CreateViewForEntityFromAddresable(
            IEntityViewAdapter<TBehaviour, TEntity> adapter,
            TEntity entity);

        TBehaviour CreateFromPrefab(
            IEntityViewAdapter<TBehaviour, TEntity> adapter, 
            TEntity entity);
    }
}