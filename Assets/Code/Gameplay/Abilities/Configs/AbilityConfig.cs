using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Abilities.Configs
{
  [CreateAssetMenu(menuName = "Custom/Abilities/AbilityConfig", fileName = "AbilityConfig")]
  public class AbilityConfig : ScriptableObject
  {
    public AbilityId AbilityId;
    public List<AbilityLevel> Levels;
  }
}