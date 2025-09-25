using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Cameras.Systems
{
    public class CreatePhysicsBordersSystem: ReactiveSystem<GameEntity>
    {
       
        private const float WallThickness = 1.0f;

        public CreatePhysicsBordersSystem(GameContext game) : base(game) { }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher
                .AllOf(
                    GameMatcher.MainCamera,
                    GameMatcher.Camera)
                .Added());

        protected override bool Filter(GameEntity camera) => camera.Camera;

        protected override void Execute(List<GameEntity> cameras)
        {
            foreach (GameEntity camera in cameras)
            {
                CreateBorders(camera.Camera);
            }
        }
        
        private void CreateBorders(Camera camera)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = camera.orthographicSize * 2;
            float cameraWidth = cameraHeight * screenAspect;
            
            Vector2 leftWallPos = new Vector2(camera.transform.position.x - cameraWidth / 2 - WallThickness / 2, camera.transform.position.y);
            Vector2 rightWallPos = new Vector2(camera.transform.position.x + cameraWidth / 2 + WallThickness / 2, camera.transform.position.y);
            Vector2 topWallPos = new Vector2(camera.transform.position.x, camera.transform.position.y + cameraHeight / 2 + WallThickness / 2);
            Vector2 bottomWallPos = new Vector2(camera.transform.position.x, camera.transform.position.y - cameraHeight / 2 - WallThickness / 2);

            // Создаем стены
            CreateWall(leftWallPos, new Vector2(WallThickness, cameraHeight));
            CreateWall(rightWallPos, new Vector2(WallThickness, cameraHeight));
            CreateWall(topWallPos, new Vector2(cameraWidth, WallThickness));
            CreateWall(bottomWallPos, new Vector2(cameraWidth, WallThickness));
        }

        private void CreateWall(Vector2 position, Vector2 size)
        {
            GameObject wall = new GameObject("Wall");
            wall.transform.position = position;

            BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
            collider.size = size;
        }


    }
}