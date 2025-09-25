using System;
using System.Linq;
using System.Text;
using Code.Audios.Audio;
using Code.Common.Entity.ToStrings;
using Code.Common.Extensions;
using Code.Gameplay.Cameras;
using Code.Gameplay.Chicken;
using Entitas;
using UnityEngine;

// ReSharper disable once CheckNamespace
public sealed partial class GameEntity : INamedEntity
{
    private EntityPrinter _printer;

    public override string ToString()
    {
        if (_printer == null)
            _printer = new EntityPrinter(this);

        _printer.InvalidateCache();

        return _printer.BuildToString();
    }

    public string EntityName(IComponent[] components)
    {
        try
        {
            if (components.Length == 1)
                return components[0].GetType().Name;

            foreach (IComponent component in components)
            {
                switch (component.GetType().Name)
                {
                    case nameof(Chicken):
                        return PrintChicken();
                    
                    case nameof(CameraComponent):
                        return PrintCamera();
                }
            }
        }
        catch (Exception exception)
        {
            Debug.LogError(exception.Message);
        }

        return components.First().GetType().Name;
    }

    private string PrintChicken() => 
        new StringBuilder($"Chicken ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    
    
    private string PrintCamera() =>
        new StringBuilder($"Camera ")
            .With(s => s.Append($"Id:{Id}"), when: hasId)
            .ToString();
    
    public string BaseToString() => base.ToString();
}