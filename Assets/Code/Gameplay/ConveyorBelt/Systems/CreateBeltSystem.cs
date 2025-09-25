using Code.Gameplay.Buildings.Configs;
using Code.Gameplay.ConveyorBelt.Factories;
using Entitas;

namespace Code.Gameplay.ConveyorBelt.Systems
{
    public class CreateBeltSystem : IExecuteSystem
    {
        private readonly IConveyourBeltFactory _beltFactory;
        private readonly IGroup<GameEntity> _buildings;
        private bool isCreated;

        public CreateBeltSystem(GameContext game, IConveyourBeltFactory beltFactory)
        {
            _beltFactory = beltFactory;
            _buildings = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Building,
                GameMatcher.BuildingTypeId
            ));
        }

        public void Execute()
        {
            if (isCreated) return;
            GameEntity barracks = null;
            GameEntity bank = null;
            GameEntity pyramid = null;
            GameEntity relics = null;
            foreach (GameEntity building in _buildings)
            {
                switch (building.BuildingTypeId)
                {
                    case BuildingTypeId.Barracks:
                        barracks = building;
                        break;
                    case BuildingTypeId.Bank:
                        bank = building;
                        break;
                    case BuildingTypeId.Pyramid:
                        pyramid = building;
                        break;
                    case BuildingTypeId.Relics:
                        relics = building;
                        break;
                }
            }
            if (barracks != null)
            {
                if (bank != null)
                {
                    _beltFactory.CreateBelt(
                        barracks.WorldPosition,
                        barracks.WorldPosition,
                        bank.WorldPosition,
                        BuildingTypeId.Bank
                    );
                }
                if (pyramid != null)
                {
                    _beltFactory.CreateBelt(
                        barracks.WorldPosition,
                        barracks.WorldPosition,
                        pyramid.WorldPosition,
                        BuildingTypeId.Pyramid
                    );
                }
                if (relics != null)
                {
                    _beltFactory.CreateBelt(
                        barracks.WorldPosition,
                        barracks.WorldPosition,
                        relics.WorldPosition,
                        BuildingTypeId.Relics
                    );
                }
            }
            isCreated = true;
        }
    }
}