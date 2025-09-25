using System.Collections.Generic;
using UnityEngine;

namespace Code.Meta.Shop.Configs
{
    [CreateAssetMenu(menuName = "Custom/Shop/ShopConfig", fileName = "ShopConfig")]
    public class ShopConfig: ScriptableObject
    {
        public List<ShopData> ShopDatas;
    }
}