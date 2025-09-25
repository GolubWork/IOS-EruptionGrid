using Entitas;
using UnityEngine;

namespace Code.Common.Destruct.Systems
{
    public class CleanupAudioDestructedViewSystem: ICleanupSystem
    {
        private readonly IGroup<AudioEntity> _entities;

        public CleanupAudioDestructedViewSystem(AudioContext context)
        {
            _entities = context.GetGroup(AudioMatcher.AllOf(
                AudioMatcher.Destructed,
                AudioMatcher.View
            ));
        }

        public void Cleanup()
        {
            foreach (AudioEntity entity in _entities)
            {
                entity.View.ReleaseEntity();
                Object.Destroy(entity.View.gameObject);
            }
        }
    }
}