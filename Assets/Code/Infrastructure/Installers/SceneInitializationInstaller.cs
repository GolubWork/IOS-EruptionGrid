using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Infrastructure.DependencyInjection; // расширения BindInterfacesTo(Type)

namespace Code.Infrastructure.Installers
{
    // Запускаемся ПОСЛЕ MonoInstaller (-32000) и KeepAlive (-31999), но ещё рано
    [DefaultExecutionOrder(-31980)]
    public class SceneInitializationInstaller : MonoBehaviour
    {
        public List<MonoBehaviour> Initializers;

        private bool _installed;

        private void Awake()
        {
            // пробуем сразу
            if (!TryInstall())
                StartCoroutine(InstallWhenReady()); // если ещё нет DI — подождём
        }

        private IEnumerator InstallWhenReady()
        {
            // ждём, пока DiContext поднимется
            while (DiContext.Instance == null || DiContext.Instance.Container == null)
                yield return null;

            TryInstall(); // теперь точно установим
        }

        private bool TryInstall()
        {
            if (_installed) return true;

            var ctx = DiContext.Instance;
            if (ctx == null || ctx.Container == null)
                return false; // ещё рано — подождём в корутине

            var c = ctx.Container;

            if (Initializers != null)
            {
                foreach (var initializer in Initializers)
                {
                    if (initializer == null) continue;

                    var type = initializer.GetType();
                    var ifaces = type.GetInterfaces();
                    if (ifaces.Length > 0)
                    {
                        // Требует наших динамических экстеншенов: c.BindInterfacesTo(Type)
                        c.BindInterfacesTo(type).FromInstance(initializer).AsSingle();
                    }
                    else
                    {
                        Debug.LogWarning($"[SceneInitializationInstaller] {type.Name} не реализует интерфейсов — пропускаю.");
                    }
                }
            }

            _installed = true;
            return true;
        }
    }
}
