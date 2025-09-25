using Code.Infrastructure.EntityViews.Behaviours;
using UnityEngine;

namespace Code.Infrastructure.EntityViews.Adapter.Adapters
{
    public abstract class EntityViewAdapter<TBehaviour, TEntity>: IEntityViewAdapter<TBehaviour, TEntity>
        where TBehaviour : Component, IEntityBehaviour<TEntity>
        where TEntity : Entitas.Entity
    {
        protected readonly TEntity Entity;

        protected EntityViewAdapter(TEntity entity)
        {
            Entity = entity;
        }

        public abstract string ViewPath { get; }
        public abstract TBehaviour ViewPrefab { get; }
    }
}