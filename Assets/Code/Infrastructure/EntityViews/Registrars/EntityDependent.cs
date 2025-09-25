using Code.Infrastructure.EntityViews.Behaviours;
using UnityEngine;

namespace Code.Infrastructure.EntityViews.Registrars
{
    public abstract class EntityDependent<TBehaviour, TEntity>: MonoBehaviour 
        where TBehaviour: IEntityBehaviour<TEntity>
        where TEntity: Entitas.Entity
    {
        public TBehaviour EntityView;
        protected TEntity Entity => EntityView != null ? EntityView.Entity : null;

        private void Awake() =>
            EntityView ??= GetComponent<TBehaviour>();
        
    }
}