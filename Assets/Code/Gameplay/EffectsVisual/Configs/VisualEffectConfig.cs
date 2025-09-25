using System;
using Code.Audios.Audio;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using UnityEngine;

namespace Code.Gameplay.EffectsVisual.Configs
{
    [Serializable]
    public class VisualEffectConfig
    {
        public VisualEffectTypeId effectTypeId;
        public GameEntityBehaviour prefab;
        public SoundTypeId effectSound;
        public float selfDestructTimer;
    }
}