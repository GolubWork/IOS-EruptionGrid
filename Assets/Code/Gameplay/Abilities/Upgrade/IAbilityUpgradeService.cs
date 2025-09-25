namespace Code.Gameplay.Abilities.Upgrade
{
  public interface IAbilityUpgradeService
  {
   // void UpgradeAbility(AbilityId ability);

   void InitializeEnemyAbility(int enemyId, AbilityId ability, int minLevel, int maxLevel);
    //void InitializeAbility(AbilityId ability);
   // List<AbilityUpgradeOption> GetUpgradeOptions();
   // int GetAbilityLevel(AbilityId abilityId);
    int GetAbilityLevel(int enemyId, AbilityId abilityId);
    void Cleanup();
  }
}