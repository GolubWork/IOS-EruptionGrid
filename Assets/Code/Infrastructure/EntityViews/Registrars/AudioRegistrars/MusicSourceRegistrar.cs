using Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours;
using UnityEngine;

namespace Code.Infrastructure.EntityViews.Registrars.AudioRegistrars
{
    public sealed class MusicSourceRegistrar : EntityComponentRegistrar<AudioEntityBehaviour, AudioEntity>
    {
        [SerializeField] private AudioSource audioSource;
        
        public override void RegisterComponents()
        {
            if (!Entity.hasMusicSource)
                Entity.AddMusicSource(audioSource);
        }

        public override void UnRegisterComponents()
        {
            if (Entity.hasMusicSource)
                Entity.RemoveMusicSource();
        }
    }
}