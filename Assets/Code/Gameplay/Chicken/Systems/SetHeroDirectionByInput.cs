using Entitas;

namespace Code.Gameplay.Chicken.Systems
{
    public class SetChickenDirectionByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<InputEntity> _inputs;

        public SetChickenDirectionByInputSystem(GameContext game, InputContext input)
        {
            _heroes = game.GetGroup(GameMatcher.Chicken);
            _inputs = input.GetGroup(InputMatcher.Input);
        }
    
        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            foreach (GameEntity hero in _heroes)
            {
                hero.isMoving = input.hasAxisInput;

                if (input.hasAxisInput) 
                    hero.ReplaceDirection(input.AxisInput.normalized);
            }
        }
    }
}