using System.Collections.Generic;
using UnityEngine;

namespace Code.Meta.Skins.Configs
{
    [CreateAssetMenu(menuName = "Custom/Skins/SkinConfig", fileName = "SkinConfig")]
    public class SkinConfig: ScriptableObject
    {
        public List<SkinData> SkinDatas;
    }
}