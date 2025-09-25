using Code.Common.Entity;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Code.Infrastructure.Identifiers;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.EntityViews
{
    public class SelfInitializedEntityView : MonoBehaviour
    {
        public GameEntityBehaviour gameEntityBehaviour;
        private IIdentifierService _identifires;

        [Inject]
        private void Construct(IIdentifierService identifiers) => _identifires = identifiers;

        
        private void Awake()
        {
           GameEntity entity = CreateGameEntity.Empty()
                .AddId(_identifires.Next());
            
            gameEntityBehaviour.SetEntity(entity);
        }
    }
}