using System.Reflection;
using Code.Infrastructure.EntityViews.Behaviours;
using UnityEngine;

namespace Code.Infrastructure.EntityViews.Registrars
{
    public abstract class AutoComponentRegistrar<TBehaviour,TEntity, TComponent>: EntityComponentRegistrar<TBehaviour, TEntity>
    where TBehaviour: IEntityBehaviour<TEntity>
    where TEntity: Entitas.Entity
    where TComponent: UnityEngine.Component
    {
        [SerializeField] protected TComponent Component;
        private string _componentName;
        private MethodInfo _addMethod;
        private MethodInfo _removeMethod;
        private PropertyInfo _hasProperty;
        
        protected void Awake()
        {
            CacheMethods();
        }

        public override void RegisterComponents()
        {
            _addMethod?.Invoke(Entity, new object[] { Component });
        }

        public override void UnRegisterComponents()
        {
            if (_hasProperty != null && (bool)_hasProperty.GetValue(Entity))
                _removeMethod?.Invoke(Entity, null);
        }
        
        private void CacheMethods()
        {
            _componentName = typeof(TComponent).Name.Replace("Component", "");
            
            _addMethod = typeof(TEntity).GetMethod($"Replace{_componentName}");
            _removeMethod = typeof(TEntity).GetMethod($"Remove{_componentName}");
            _hasProperty = typeof(TEntity).GetProperty($"has{_componentName}");
        }
    }
}