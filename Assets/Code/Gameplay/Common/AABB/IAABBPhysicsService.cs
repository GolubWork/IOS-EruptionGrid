using UnityEngine;

namespace Code.Gameplay.Common.AABB
{
    public interface IAABBPhysicsService
    {
        bool IsColliding(GameEntity entityA, GameEntity entityB);
        bool IsWithinCameraBounds(Vector2 position, Vector2 size, GameEntity camera);
        ((bool xWithinBounds, float xOutDistance), (bool yWithinBounds, float yOutDistance))  XYWithinCameraBounds(Vector2 position, Vector2 size, GameEntity camera);
        Vector2 GetSpriteSize(SpriteRenderer spriteRenderer);
    }
}