using Code.Gameplay.Effects.Configs;

namespace Code.Gameplay.Effects.Factory
{
    public interface IEffectFactory
    {
        GameEntity CreateEffect(EffectSetup setup, int producerId, int targetId);
        GameEntity CreateEffect(EffectConfig setup, int producerId, int targetId);
    }
}