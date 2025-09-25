using UnityEngine;

namespace Code.Gameplay.StaticData.AdditionalSpriteProvider
{
    [CreateAssetMenu(fileName = "AdditionalSpriteConfig", menuName = "Custom/Sprites/AdditionalSpriteConfig")]
    public class AdditionalSpriteConfig: ScriptableObject
    {
        public Sprite Shelf;
        public Sprite Bucket;
        public Sprite Card;
        public Sprite GridCell;
        public Sprite MirrorCell;
        public Sprite CellFiller;
    }
}