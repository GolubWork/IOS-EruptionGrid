using Code.Gameplay.EffectsVisual.Configs;
using UnityEngine;

namespace Code.Gameplay.EffectsVisual.Factories
{
    public interface IVisualEffectFactory
    {
        GameEntity CreateVisualEffect(VisualEffectConfig config, int producerId, int targetId, Vector3 at);
    }
}