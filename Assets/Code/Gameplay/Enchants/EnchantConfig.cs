using System.Collections.Generic;
using Code.Gameplay.Effects;
using Code.Gameplay.Statuses;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using UnityEngine;

namespace Code.Gameplay.Enchants
{
  [CreateAssetMenu(menuName = "Custom/Abilities/Enchant Config", fileName = "EnchantConfig")]
  public class EnchantConfig : ScriptableObject
  {
    public EnchantTypeId TypeId;

    public Sprite Icon;
    
    public List<EffectSetup> EffectSetups;
    public List<StatusSetup> StatusSetups;
    
    public float Radius;
    public GameEntityBehaviour ViewPrefab;
  }
}