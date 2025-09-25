using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UpdateCurrentLerpSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _belts;

    public UpdateCurrentLerpSystem(GameContext game)
    {
        _belts = game.GetGroup(GameMatcher.AllOf(
            GameMatcher.ConveyorBelt,
            GameMatcher.LineRenderer,
            GameMatcher.ConveyourItemsIds
        ));
    }

    public void Execute()
    {
        foreach (GameEntity belt in _belts)
        {
            var itemsToRemove = new List<int>();
            for (int i = 0; i < belt.ConveyourItemsIds.Count; i++)
            {
                int itemId = belt.ConveyourItemsIds[i];
                GameEntity item = GetEntityWithId(itemId);
                
                if (item == null || item.isDestructed)
                {
                    itemsToRemove.Add(itemId);
                    continue;
                }

                if (i > 0)
                {
                    GameEntity prevItem = GetEntityWithId(belt.ConveyourItemsIds[i - 1]);
                    if (prevItem == null || prevItem.isDestructed)
                    {
                        itemsToRemove.Add(belt.ConveyourItemsIds[i - 1]);
                        continue;
                    }
                    
                    if (Vector3.Distance(item.WorldPosition, prevItem.WorldPosition) <= belt.ItemSpacing)
                    {
                        continue;
                    }
                }
                
                if (item.isReachedEnd) continue;

                item.Transform.position = Vector3.Lerp(
                    belt.LineRenderer.GetPosition(item.StartPoint),
                    belt.LineRenderer.GetPosition(item.StartPoint + 1), 
                    item.CurrentLerp);
                item.ReplaceWorldPosition(item.Transform.position);
                
                float distance = Vector3.Distance(
                    belt.LineRenderer.GetPosition(item.StartPoint),
                    belt.LineRenderer.GetPosition(item.StartPoint + 1));
                item.ReplaceDistance(distance);

                float speed = item.CurrentLerp + (belt.ConveyorBeltSpeed * Time.deltaTime) / item.Distance;
                item.ReplaceCurrentLerp(speed);
            }
            if (itemsToRemove.Count > 0)
            {
                var updatedItems = new List<int>(belt.ConveyourItemsIds);
                foreach (int itemId in itemsToRemove)
                {
                    updatedItems.Remove(itemId);
                }
                belt.ReplaceConveyourItemsIds(updatedItems);
            }
        }
    }

    private GameEntity GetEntityWithId(int id) => Contexts.sharedInstance.game.GetEntityWithId(id);
}