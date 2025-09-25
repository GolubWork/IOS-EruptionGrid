using Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours;

namespace Code.Infrastructure.EntityViews.Adapter.Adapters.AudioEntityViewAdapters
{
    public class AudioEntityViewAdapter: EntityViewAdapter<AudioEntityBehaviour, AudioEntity>
    {
        public AudioEntityViewAdapter(AudioEntity entity) : base(entity) { }
        public override string ViewPath => Entity.hasViewPath ? Entity.ViewPath : string.Empty;
        public override AudioEntityBehaviour ViewPrefab => Entity.hasViewPrefab ? Entity.ViewPrefab : null;
    }
}