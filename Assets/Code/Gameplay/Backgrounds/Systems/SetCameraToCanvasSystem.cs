using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Backgrounds.Systems
{
    public class SetCameraToCanvasSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _cameras;
        private readonly IGroup<GameEntity> _backgrounds;
        private List<GameEntity> _buffer = new (1);

        public SetCameraToCanvasSystem(GameContext game)
        {
            _cameras = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Camera,
                GameMatcher.MainCamera
            ));
            
            _backgrounds = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Background,
                GameMatcher.Canvas
                ).NoneOf(GameMatcher.Processed));
        }


        public void Execute()
        {
            foreach (GameEntity background in _backgrounds.GetEntities(_buffer))
            foreach (GameEntity camera in _cameras)
            {
                background.Canvas.worldCamera = camera.Camera;
                background.isProcessed = true;
            }
        }
    }
}