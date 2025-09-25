using Code.Infrastructure.EntityViews.Behaviours;

namespace Code.Infrastructure.EntityViews.Registrars
{
    public abstract class EntityComponentRegistrar<TBehaviour, TEntity>: EntityDependent<TBehaviour,TEntity>, IEntityComponentRegistrar
        where TBehaviour : IEntityBehaviour<TEntity>
        where TEntity : Entitas.Entity
    {
        public abstract void RegisterComponents();
        public abstract void UnRegisterComponents();
    }
}