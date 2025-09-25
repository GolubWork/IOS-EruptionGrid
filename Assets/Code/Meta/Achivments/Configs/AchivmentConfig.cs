using System.Collections.Generic;
using UnityEngine;

namespace Code.Meta.Achivments.Configs
{
    [CreateAssetMenu(menuName = "Custom/Achivments/AchivmentConfig", fileName = "AchivmentConfig")]
    public class AchivmentConfig: ScriptableObject
    {
        public List<AchivmentData> Achivments;
    }
}