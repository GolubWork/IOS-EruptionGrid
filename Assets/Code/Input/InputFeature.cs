using Code.Infrastructure.Systems;
using Code.Input.Systems;

namespace Code.Input
{
    public class InputFeature : Feature
    {
        public InputFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeInputSystem>());
            
           // Add(systems.Create<EmitTouchInputSystem>());
            
            //Add(systems.Create<EmitInputSystem>());
        }
    }
}