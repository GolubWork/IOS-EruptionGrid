using UnityEngine;

namespace Code.Gameplay.Cameras.Factory
{
    public interface ICameraFactory
    {
        GameEntity CreateCamera(Vector3 at);
        GameEntity CreateBorderCamera(Vector3 at);
    }
}