using Code.Gameplay.Physics.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Physics.Systems
{
    public class CreateHeroForceOnSpacePressed: IExecuteSystem
    {
        private readonly IForceFactory _forceFactory;
        private readonly IGroup<GameEntity> _heroes;

        public CreateHeroForceOnSpacePressed(GameContext gameContext, IForceFactory forceFactory)
        {
            _forceFactory = forceFactory;
            _heroes = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Chicken,
                GameMatcher.PhysicsBody
            ));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                {
                    _forceFactory.CreateForce(-1, hero.Id, new Vector2(1,1));
                }
            }
        }

    }
}