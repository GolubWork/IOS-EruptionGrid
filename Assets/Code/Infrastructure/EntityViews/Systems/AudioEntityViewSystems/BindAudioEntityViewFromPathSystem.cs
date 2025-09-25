using System.Collections.Generic;
using Code.Infrastructure.EntityViews.Adapter.Adapters.AudioEntityViewAdapters;
using Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours;
using Code.Infrastructure.EntityViews.Fabrics;
using Entitas;

namespace Code.Infrastructure.EntityViews.Systems.AudioEntityViewSystems
{
    public class BindAudioEntityViewFromPathSystem : IExecuteSystem
    {
        private readonly IEntityViewFactory<AudioEntityBehaviour, AudioEntity> _audioEntityViewFactory;
        private readonly IGroup<AudioEntity> _entities;
        private readonly List<AudioEntity> _buffer = new(32);

        public BindAudioEntityViewFromPathSystem(AudioContext contextParameter, IEntityViewFactory<AudioEntityBehaviour, AudioEntity> audioEntityViewFactory)
        {
            _audioEntityViewFactory = audioEntityViewFactory;
            _entities = contextParameter.GetGroup(AudioMatcher
                .AllOf(AudioMatcher.ViewPath)
                .NoneOf(
                    AudioMatcher.View,
                    AudioMatcher.ViewProcessed)
            );
        }

        public void Execute()
        {
            foreach (AudioEntity entity in _entities.GetEntities(_buffer))
            {
                _audioEntityViewFactory.CreateViewForEntityFromAddresable(new AudioEntityViewAdapter(entity), entity);
                entity.isViewProcessed = true;
            }
        }
    }
}