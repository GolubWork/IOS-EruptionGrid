using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;

namespace Code.Infrastructure.EntityViews.Adapter.Adapters.GameEntityViewAdapters
{
    public class GameEntityViewAdapter : EntityViewAdapter<GameEntityBehaviour, GameEntity>
    {
        public GameEntityViewAdapter(GameEntity entity) : base(entity) { }
        public override string ViewPath => Entity.hasViewPath ? Entity.ViewPath : string.Empty;
        public override GameEntityBehaviour ViewPrefab => Entity.hasViewPrefab ? Entity.ViewPrefab : null;
    }
}