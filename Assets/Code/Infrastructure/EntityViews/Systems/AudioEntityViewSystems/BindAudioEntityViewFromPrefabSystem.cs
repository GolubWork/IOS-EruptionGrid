using System.Collections.Generic;
using Code.Infrastructure.EntityViews.Adapter.Adapters.AudioEntityViewAdapters;
using Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours;
using Code.Infrastructure.EntityViews.Fabrics;
using Entitas;

namespace Code.Infrastructure.EntityViews.Systems.AudioEntityViewSystems
{
    public class BindAudioEntityViewFromPrefabSystem : IExecuteSystem
    {
        private readonly IEntityViewFactory<AudioEntityBehaviour, AudioEntity> _gameEntityViewFactory;
        private readonly IGroup<AudioEntity> _entities;
        private readonly List<AudioEntity> _buffer = new(32);

        public BindAudioEntityViewFromPrefabSystem(AudioContext contextParameter, IEntityViewFactory<AudioEntityBehaviour, AudioEntity> gameEntityViewFactory)
        {
            _gameEntityViewFactory = gameEntityViewFactory;
            _entities = contextParameter.GetGroup(AudioMatcher
                .AllOf(AudioMatcher.ViewPrefab)
                .NoneOf(AudioMatcher.View)
            );
        }

        public void Execute()
        {
            foreach (AudioEntity entity in _entities.GetEntities(_buffer))
            {
                _gameEntityViewFactory.CreateFromPrefab(new AudioEntityViewAdapter(entity), entity);
            }
        }
    }
}