using Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours;
using UnityEngine;

namespace Code.Infrastructure.EntityViews.Registrars.AudioRegistrars
{
    public sealed class SoundSourceRegistrar : EntityComponentRegistrar<AudioEntityBehaviour, AudioEntity>
    {
        [SerializeField] private AudioSource audioSource;
        
        public override void RegisterComponents()
        {
            if (!Entity.hasSoundSource)
                Entity.AddSoundSource(audioSource);
        }

        public override void UnRegisterComponents()
        {
            if (Entity.hasSoundSource)
                Entity.RemoveSoundSource();
        }
    }
}