using Code.Gameplay.Common.Collisions;
using Code.Infrastructure.EntityViews.Registrars;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours
{
    public sealed class AudioEntityBehaviour: MonoBehaviour, IEntityBehaviour<AudioEntity>
    {
        private AudioEntity _entity;
        private ICollisionRegistry _collisionRegistry;
        public AudioEntity Entity => _entity;

        [Inject]
        private void Construct(ICollisionRegistry collisionRegistry) => 
            _collisionRegistry = collisionRegistry;
        
        public void SetEntity(AudioEntity entity)
        {
            _entity = entity;
            _entity.AddView(this);
            _entity.Retain(this);

            foreach(IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.RegisterComponents();

            foreach (Collider2D collider2D in GetComponentsInChildren<Collider2D>(includeInactive:true))
                _collisionRegistry.Register(collider2D.GetInstanceID(), _entity);
        }

        public void ReleaseEntity()
        {
            foreach(IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
                registrar.UnRegisterComponents();
            
            foreach (Collider2D collider2D in GetComponentsInChildren<Collider2D>(includeInactive:true))
                _collisionRegistry.Unregister(collider2D.GetInstanceID());
            
            _entity.Release(this);
            _entity = null;
        }
    }
}