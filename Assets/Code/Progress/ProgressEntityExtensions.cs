using System;
using System.Linq;
using Code.Common.Extensions;
using Entitas;

namespace Code.Progress
{
  public static class ProgressEntityExtensions
  {
    public static IEntity HydrateWith(this IEntity entity, EntitySnapshot entityData)
    {
      foreach (ISavedComponent component in entityData.Components)
      {
        int lookupIndex = LookupIndexOf(component, entity);
        entity.With(x => x.ReplaceComponent(lookupIndex, component), when: lookupIndex >= 0);
      }
      return entity;
    }

    private static int LookupIndexOf(ISavedComponent component, IEntity entity) => 
      Array.IndexOf(ComponentTypes(entity), component.GetType());

    
    private static Type[] ComponentTypes(IEntity entity) =>
      entity switch
      {
        MetaEntity => MetaComponentsLookup.componentTypes,
        GameEntity => GameComponentsLookup.componentTypes,
        InputEntity => InputComponentsLookup.componentTypes,
        AudioEntity => AudioComponentsLookup.componentTypes,
        _ => throw new ArgumentException($"Lookup for {entity.GetType().Name} is not implemented")
      };

    public static EntitySnapshot AsSavedEntity(this IEntity entity)
    {
      IComponent[] components = entity.GetComponents();

      return new EntitySnapshot
      {
        Components = components
          .Where(c => c is ISavedComponent)
          .Cast<ISavedComponent>()
          .ToList()
      };
    }
  }
}