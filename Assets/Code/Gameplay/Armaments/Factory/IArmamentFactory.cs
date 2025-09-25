using UnityEngine;

namespace Code.Gameplay.Armaments.Factory
{
  public interface IArmamentFactory
  {
    GameEntity CreateMeteor(int level, Vector3 at);
  }
}