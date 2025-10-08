using System.Collections;
using System.Collections.Generic;
using Code.Common.Helpers;
using UnityEngine;
using Code.Infrastructure.DependencyInjection; 

namespace Code.Infrastructure.Installers
{
    [DefaultExecutionOrder(-31980)]
    public class SceneInitializationInstaller : MonoBehaviour
    {
        public List<MonoBehaviour> Initializers;

        private bool _installed;

        private void Awake()
        {
            if (!TryInstall())
                StartCoroutine(InstallWhenReady());
        }

        private IEnumerator InstallWhenReady()
        {
            while (DiContext.Instance == null || DiContext.Instance.Container == null)
                yield return null;

            TryInstall();
        }

        private bool TryInstall()
        {
            if (_installed) return true;

            var ctx = DiContext.Instance;
            if (ctx == null || ctx.Container == null)
                return false;

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
                        c.BindInterfacesTo(type).FromInstance(initializer).AsSingle();
                    }
                    else
                    {
                        CustomDebug.LogWarning($"[SceneInitializationInstaller] {type.Name} не реализует интерфейсов — пропускаю.");
                    }
                }
            }

            _installed = true;
            return true;
        }
    }
}
