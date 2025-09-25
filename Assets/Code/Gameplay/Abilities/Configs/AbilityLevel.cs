using System;
using System.Collections.Generic;
using Code.Gameplay.Effects;
using Code.Gameplay.Statuses;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using UnityEngine;

namespace Code.Gameplay.Abilities.Configs
{
  [Serializable]
  public class AbilityLevel
  {
    public Sprite Icon;
    public string Description;
    
    public float Cooldown;

    public GameEntityBehaviour ViewPrefab;
    public GameEntityBehaviour CollisionEffectPrefab;

    public List<EffectSetup> EffectSetups;
    public List<StatusSetup> StatusSetups;
    
    public ProjectileSetup ProjectileSetup;
    public AuraSetup AuraSetup;
  }
}