using System;
using System.Collections.Generic;
using Code.Gameplay.Abilities.Factory;
using Code.Gameplay.Common.Random;

namespace Code.Gameplay.Abilities.Upgrade
{
  public class AbilityUpgradeService : IAbilityUpgradeService
  {

    private readonly Dictionary<int, Dictionary<AbilityId, int>> _enemyAbilities;
    
    private readonly IRandomService _random;
    private readonly IAbilityFactory _abilityFactory;

    public AbilityUpgradeService(IRandomService randomService, IAbilityFactory abilityFactory)
    {
      _enemyAbilities = new Dictionary<int, Dictionary<AbilityId, int>>();
      _random = randomService;
      _abilityFactory = abilityFactory;
    }
    
    public int GetAbilityLevel(int enemyId, AbilityId abilityId)
    {
      if (_enemyAbilities.TryGetValue(enemyId, out var abilities))
      {
        return abilities.TryGetValue(abilityId, out int level) ? level : 0;
      }
      return 0;
    }
    public void InitializeEnemyAbility(int enemyId, AbilityId ability, int minLevel, int maxLevel)
    {
    
      if (!_enemyAbilities.TryGetValue(enemyId, out var abilities))
      {
        abilities = new Dictionary<AbilityId, int>();
        _enemyAbilities[enemyId] = abilities;
      }
      int randomLevel = _random.Range(minLevel, maxLevel + 1);
      if (!abilities.TryAdd(ability, randomLevel))
      {
        throw new Exception($"Ability {ability} for enemy {enemyId} is already initialized.");
      }
      switch (ability)
      {
        case AbilityId.Meteor:
          _abilityFactory.MeteorAbility(enemyId, randomLevel);
          break;
        default:
          throw new Exception($"Ability {ability} is not defined.");
      }
    }
    public void Cleanup()
    {
      _enemyAbilities.Clear();
    }
  }
}