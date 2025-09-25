using Code.Meta.Skins.Configs;
using Code.Progress;
using Entitas;

namespace Code.Meta.Skins
{
    [Meta] public class SelectedSkinStorage : ISavedComponent { public SkinTypeId Value; }
    [Meta] public class ChangeSkinRequest : IComponent {  }
    
    [Meta] public class RequestSkinTypeId : IComponent { public SkinTypeId Value;  }
    [Game] public class RequireSkinApplication : IComponent {  }
    
    
}