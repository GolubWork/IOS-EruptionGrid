using UnityEngine;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Утилиты для запуска инициализации на объектах после DI.
    /// Можно вызывать вручную из кода или использовать AutoRunner (ниже).
    /// </summary>
    public static class InitializableUtility
    {
        /// <summary>Запустить Initialize/LateInitialize на одном объекте, если он их реализует.</summary>
        public static void RunOn(object instance)
        {
            if (instance == null) return;

            // Сначала IInitializable
            var init = instance as IInitializable;
            if (init != null) init.Initialize();

            // Затем ILateInitializable
            var late = instance as ILateInitializable;
            if (late != null) late.LateInitialize();
        }

        /// <summary>
        /// Пройтись по всем компонентам на GO (и дочерним при необходимости) и вызвать Initialize/LateInitialize.
        /// Полезно вызывать сразу после DiContext.InjectGameObject(...).
        /// </summary>
        public static void RunOnGameObject(GameObject root, bool includeChildren = true)
        {
            if (root == null) return;

            if (includeChildren)
            {
                var comps = root.GetComponentsInChildren<Component>(includeInactive: true);
                // 1-й проход — Initialize
                foreach (var c in comps)
                    (c as IInitializable)?.Initialize();
                // 2-й проход — LateInitialize
                foreach (var c in comps)
                    (c as ILateInitializable)?.LateInitialize();
            }
            else
            {
                var comps = root.GetComponents<Component>();
                foreach (var c in comps)
                    (c as IInitializable)?.Initialize();
                foreach (var c in comps)
                    (c as ILateInitializable)?.LateInitialize();
            }
        }
    }
}