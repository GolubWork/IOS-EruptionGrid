using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.StaticData.RandomSpriteStaticData
{
    [CreateAssetMenu(fileName = "SpriteConfig", menuName = "Custom/Sprites/RandomSpriteConfig")]
    public class SpriteConfig: ScriptableObject
    {
        public List<Sprite> sprites;
    }
}