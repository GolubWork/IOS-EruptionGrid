using System.Collections.Generic;
using Entitas;

namespace Code.Common.Destruct.Systems
{
    public class CleanupAudioDestructedSystem: ICleanupSystem
    {
        private readonly IGroup<AudioEntity> _entities;
        private readonly List<AudioEntity> _buffer = new(64);

        public CleanupAudioDestructedSystem(AudioContext gameContext) => 
            _entities =  gameContext.GetGroup(AudioMatcher.Destructed);
        

        public void Cleanup()
        {
            foreach (AudioEntity entity in _entities.GetEntities(_buffer))
                entity.Destroy();
        }
    }
}