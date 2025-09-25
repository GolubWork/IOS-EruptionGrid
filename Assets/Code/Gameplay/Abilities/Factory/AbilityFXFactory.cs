using Code.Common.Entity;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Abilities.Factory
{
    public class AbilityFXFactory : IAbilityFXFactory
    {
        private readonly IIdentifierService _identifiers;
        
        public AbilityFXFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }
        public GameEntity CreateExplosiveFX(Vector3 at)
        {
            return CreateGameEntity.Empty()
                .AddId(_identifiers.Next())
                .AddWorldPosition(at)
                .AddViewPath("Gameplay/Explosions/Explosion")
                .AddSelfDestructTimer(1f);
            ;
        }
    }
}