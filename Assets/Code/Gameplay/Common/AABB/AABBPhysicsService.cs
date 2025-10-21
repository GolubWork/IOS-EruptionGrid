using Code.Common.Helpers;
using UnityEngine;

namespace Code.Gameplay.Common.AABB
{
    public class AABBPhysicsService : IAABBPhysicsService
    {
        public bool IsColliding(GameEntity entityA, GameEntity entityB)
        {
            var transformA = entityA.Transform;
            var transformB = entityB.Transform;

            if (transformA == null || transformB == null)
            {
                CustomDebug.LogError("Entities must have a Transform to check collisions.");
                return false;
            }

            // Получаем размеры с учетом Transform.localScale
            Vector2 sizeA = GetScaledSize(entityA);
            Vector2 sizeB = GetScaledSize(entityB);

            // Получаем позиции объектов
            Vector2 positionA = (Vector2)transformA.position;
            Vector2 positionB = (Vector2)transformB.position;

            // Проверяем пересечение с учетом AABB (Axis-Aligned Bounding Box)
            return IsAABBIntersecting(positionA, sizeA, positionB, sizeB);

        }
        private Vector2 GetScaledSize(GameEntity entity)
        {
            SpriteRenderer spriteRenderer = entity.SpriteRenderer; // Предполагается, что у сущности есть SpriteRenderer
            if (spriteRenderer == null)
            {
                CustomDebug.LogError($"Entity {entity} is missing SpriteRenderer! Cannot compute size.");
                return Vector2.zero;
            }

            Vector2 baseSize = GetSpriteSize(spriteRenderer);
            Vector3 localScale = entity.Transform.localScale; // Учитываем масштаб

            // Умножаем базовый размер на масштаб (по X и Y)
            return new Vector2(baseSize.x * localScale.x, baseSize.y * localScale.y);
        }

        public bool IsWithinCameraBounds(Vector2 position, Vector2 size, GameEntity camera)
        {
            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;
            Vector3 bottomLeft = camera.Camera.ViewportToWorldPoint(new Vector3(0, 0, camera.Camera.nearClipPlane));
            Vector3 topRight = camera.Camera.ViewportToWorldPoint(new Vector3(1, 1, camera.Camera.nearClipPlane));

            return  position.x - halfWidth >= bottomLeft.x && position.x + halfWidth <= topRight.x &&
                    position.y - halfHeight >= bottomLeft.y && position.y + halfHeight <= topRight.y;
        }

        public ((bool xWithinBounds, float xOutDistance), (bool yWithinBounds, float yOutDistance)) XYWithinCameraBounds(Vector2 position, Vector2 size, GameEntity camera)
        {
            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;
            Vector3 bottomLeft = camera.Camera.ViewportToWorldPoint(new Vector3(0, 0, camera.Camera.nearClipPlane));
            Vector3 topRight = camera.Camera.ViewportToWorldPoint(new Vector3(1, 1, camera.Camera.nearClipPlane));

           
            bool xWithinBounds = position.x - halfWidth >= bottomLeft.x && position.x + halfWidth <= topRight.x;
            float xOutDistance = 0;
            if (!xWithinBounds)
            {
                if (position.x - halfWidth < bottomLeft.x)
                {
                    xOutDistance = (position.x - halfWidth) - bottomLeft.x;
                }
                else if (position.x + halfWidth > topRight.x)
                {
                    xOutDistance = (position.x + halfWidth) - topRight.x;
                }
            }
            
            bool yWithinBounds = position.y - halfHeight >= bottomLeft.y && position.y + halfHeight <= topRight.y;
            float yOutDistance = 0;
            if (!yWithinBounds)
            {
                if (position.y - halfHeight < bottomLeft.y)
                {
                    yOutDistance = (position.y - halfHeight) - bottomLeft.y;
                }
                else if (position.y + halfHeight > topRight.y)
                {
                    yOutDistance = (position.y + halfHeight) - topRight.y;
                }
            }

            return ((xWithinBounds, xOutDistance), (yWithinBounds, yOutDistance));
        }
        
        public Vector2 GetSpriteSize(SpriteRenderer spriteRenderer)
        {
            Vector2 size = spriteRenderer.sprite.bounds.size;
            return new Vector2(size.x, size.y);
        }
        private bool IsAABBIntersecting(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            return positionA.x - sizeA.x / 2 < positionB.x + sizeB.x / 2 &&
                   positionA.x + sizeA.x / 2 > positionB.x - sizeB.x / 2 &&
                   positionA.y - sizeA.y / 2 < positionB.y + sizeB.y / 2 &&
                   positionA.y + sizeA.y / 2 > positionB.y - sizeB.y / 2;
        }
        private bool IsWithinBounds(Vector2 position, Vector2 size, GameEntity camera)
        {
            Vector3 bottomLeft = camera.Camera.ViewportToWorldPoint(new Vector3(0, 0, camera.Camera.nearClipPlane));
            Vector3 topRight = camera.Camera.ViewportToWorldPoint(new Vector3(1, 1, camera.Camera.nearClipPlane));

            return position.x - size.x / 2 >= bottomLeft.x && position.x + size.x / 2 <= topRight.x &&
                   position.y - size.y / 2 >= bottomLeft.y && position.y + size.y / 2 <= topRight.y;
        }
        
        public bool IsBoxCollider2DColliding(GameEntity entityA, GameEntity entityB)
        {
            if (entityA.BoxCollider2D == null || entityB.BoxCollider2D == null)
            {
                CustomDebug.LogError("Both entities must have BoxCollider2D component to check collision.");
                return false;
            }

            if (entityA.Transform == null || entityB.Transform == null)
            {
                CustomDebug.LogError("Both entities must have Transform to check collision.");
                return false;
            }

            Vector2 firstCenter = GetColliderWorldCenter(entityA);
            Vector2 firstSize = GetColliderWorldSize(entityA);

            Vector2 secondCenter = GetColliderWorldCenter(entityB);
            Vector2 secondSize = GetColliderWorldSize(entityB);

            return IsAABBIntersecting(firstCenter, firstSize, secondCenter, secondSize);
        }

        private Vector2 GetColliderWorldCenter(GameEntity entity)
        {
            var box = entity.BoxCollider2D;
            var transform = entity.Transform;

            Vector2 scaledOffset = Vector2.Scale(box.offset, transform.lossyScale);
            return (Vector2)transform.position + scaledOffset;
        }

        private Vector2 GetColliderWorldSize(GameEntity entity)
        {
            var box = entity.BoxCollider2D;
            var transform = entity.Transform;

            return new Vector2(
                box.size.x * Mathf.Abs(transform.lossyScale.x),
                box.size.y * Mathf.Abs(transform.lossyScale.y)
            );
        }
    }
}