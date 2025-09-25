using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Cards.Configs
{
    [CreateAssetMenu(menuName = "Custom/Cards/CardConfigs", fileName = "CardConfigs")]
    public class CardConfigs: ScriptableObject
    {
        public List<CardConfig> CardConfig;
    }
}