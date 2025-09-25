using Code.Infrastructure.EntityViews.Behaviours;
using UnityEngine;

namespace Code.Infrastructure.EntityViews.Adapter.Adapters
{
    public interface IEntityViewAdapter<TBehaviour, TEntity>
    where TBehaviour : Component, IEntityBehaviour<TEntity>
    where TEntity : Entitas.Entity
    {
        string ViewPath { get; }
        TBehaviour ViewPrefab { get; }
    }
}