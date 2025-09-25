using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class SceneInitializationInstaller : MonoInstaller
    {
        public List<MonoBehaviour> Initializers;
    
        public override void InstallBindings()
        {
            foreach (MonoBehaviour initializer in Initializers)
            {
                var interfaces = initializer.GetType().GetInterfaces();
                if (interfaces.Length > 0)
                {
                    Container.BindInterfacesTo(initializer.GetType()).FromInstance(initializer).AsSingle();
                }
                else
                {
                    Debug.LogWarning($"Initializer {initializer.GetType().Name} не реализует интерфейсов.");
                }
            }
        }
    }
}