using UnityEngine;
using Component = UnityEngine.Component; // IInitializable / ILateInitializable, если подключал

namespace Code.Infrastructure.DependencyInjection
{
    [DefaultExecutionOrder(-32000)]
    public abstract class MonoInstaller : DiContext
    {
        public abstract void InstallBindings();

        protected override void Awake()
        {
            base.Awake();

            // если этот компонент НЕ стал активным DiContext (дубликат) — выходим
            if (Instance != this || Container == null)
                return;

            InstallBindings();

            // --- автозапуск IInitializable/ILateInitializable на этом GO ---
            // Вариант 1: через утилиту (если файл Initializables.cs подключен)
            try { InitializableUtility.RunOnGameObject(gameObject, includeChildren: false); }
            catch
            {
                // Вариант 2: без зависимости от утилиты — локально
                var comps = GetComponents<Component>();
                foreach (var c in comps) (c as IInitializable)?.Initialize();
                foreach (var c in comps) (c as ILateInitializable)?.LateInitialize();
            }
        }
    }
}