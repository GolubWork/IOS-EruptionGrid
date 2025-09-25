using Code.Meta.Shop.Configs;
using UnityEngine;

namespace Code.Meta.Skins.Configs
{
    [System.Serializable]
    public class SkinData
    {
        public SkinShopItem shopItem;
        public SkinTypeId skinTypeId;
        public SkinStatusId skinStatusId;
        public Sprite Sprite;
    }
}