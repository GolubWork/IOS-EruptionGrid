using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Zones.Systems
{
    public class PlaceZoneSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _zones;
        private readonly IGroup<GameEntity> _cameras;
        private List<GameEntity> _buffer = new (1);

        public PlaceZoneSystem(GameContext game)
        {
            _zones = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Zone,
                GameMatcher.ZoneTypeId,
                GameMatcher.ZonePercent,
                GameMatcher.BoxCollider,
                GameMatcher.Transform
            ).NoneOf(GameMatcher.ZonePlaced));

            _cameras = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Camera,
                GameMatcher.MainCamera,
                GameMatcher.WorldPosition
            ));
        }

        public void Execute()
        {
            if(_cameras.count <= 0) return;
            foreach (GameEntity zone in _zones.GetEntities(_buffer))
            {
                Camera camera = _cameras.GetSingleEntity().Camera;
                float height = 2f * camera.orthographicSize;
                float width = height * camera.aspect;
                
                float zoneHeight = height * (zone.ZonePercent / 100f);
                float zoneY = (height / 2f) - (zoneHeight / 2f);
                Vector3 zonePosition = new Vector3(0, zoneY - 2.5f, 0);
                
                zone.ReplaceWorldPosition(zonePosition);
                zone.ReplaceZoneHeight(zoneHeight);
                zone.ReplaceZoneWidth(width);
                
                zone.BoxCollider.size = new Vector2(width, zoneHeight);
                zone.isZonePlaced = true;
            }
        }


        
    }
}