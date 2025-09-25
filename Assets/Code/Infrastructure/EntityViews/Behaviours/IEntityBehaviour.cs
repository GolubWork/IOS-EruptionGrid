using UnityEngine;

namespace Code.Infrastructure.EntityViews.Behaviours
{
    public interface IEntityBehaviour<T> 
        where T: Entitas.Entity
    {
        T Entity { get; }
        void SetEntity(T entity);
        void ReleaseEntity();

        GameObject gameObject { get; }
    }
}