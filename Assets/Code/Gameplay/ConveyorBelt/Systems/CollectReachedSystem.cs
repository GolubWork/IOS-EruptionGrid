using System.Collections.Generic;
using Code.Gameplay.Buildings.Configs;
using Code.Gameplay.Items.Configs;
using Entitas;

namespace Code.Gameplay.ConveyorBelt.Systems
{
    public class CollectReachedSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _items;
        private readonly IGroup<GameEntity> _buildings;
        private List<GameEntity> _buffer = new (1);

        public CollectReachedSystem(GameContext game)
        {
            _items = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.ReachedEnd,
                GameMatcher.ItemTypeId
            ));
            
            _buildings = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Building,
                GameMatcher.BuildingTypeId,
                GameMatcher.CurBuildingStorage
            ));
        }

        public void Execute()
        {
            foreach (GameEntity item in _items.GetEntities(_buffer))
            foreach (GameEntity building in _buildings)
            {
                foreach (ProducingItemTypeId recivingTypeId in building.RecievingItemsTypeId)
                {
                    if(recivingTypeId != item.ItemTypeId) continue;
                    building.ReplaceCurBuildingStorage(building.CurBuildingStorage + 1);
                }
                item.isDestructed = true;
            }
        }
    }
}