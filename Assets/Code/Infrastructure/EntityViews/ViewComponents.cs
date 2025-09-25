using Code.Infrastructure.EntityViews.Behaviours;
using Code.Infrastructure.EntityViews.Behaviours.AudioBehaviours;
using Code.Infrastructure.EntityViews.Behaviours.GameBehaviours;
using Entitas;

namespace Code.Infrastructure.EntityViews
{
    [Game, Audio] public class ViewProcessed : IComponent { }
    [Game, Audio] public class ViewPath : IComponent { public string Value; }

//namespace Code.Infrastructure.EntityViews.GameViews
    namespace Code.Infrastructure.EntityViews.GameViews
    {
        [Game] public class View : IComponent { public IEntityBehaviour<GameEntity> Value; }
        [Game] public class ViewPrefab : IComponent { public GameEntityBehaviour Value; }
    }
    
//namespace Code.Infrastructure.EntityViews.AudioViews
    namespace Code.Infrastructure.EntityViews.AudioViews
    {
        [Audio] public class View  : IComponent { public IEntityBehaviour<AudioEntity> Value; }
        [Audio] public class ViewPrefab : IComponent { public AudioEntityBehaviour Value; }
    }

    

}