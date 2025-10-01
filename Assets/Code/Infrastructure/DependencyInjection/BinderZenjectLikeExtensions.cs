using UnityEngine;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Extension-методы для биндеров MiniDi, имитирующие Zenject API.
    /// Позволяют писать: Container.BindInterfacesAndSelfTo<T>().FromComponentInNewPrefab(prefab).AsSingle();
    /// </summary>
    public static class BinderZenjectLikeExtensions
    {
        /// <summary>
        /// Для MultiBinder (BindInterfacesAndSelfTo / BindInterfacesTo).
        /// Инстанцирует prefab и возвращает компонент TComponent через IInstantiator,
        /// после чего этот компонент раздаётся всем сервисным ключам MultiBinder.
        /// </summary>
        public static DiContainer.MultiBinder FromComponentInNewPrefab<TComponent>(
            this DiContainer.MultiBinder multiBinder,
            UnityEngine.Object prefab,
            Vector3? position = null,
            Quaternion? rotation = null,
            Transform parent = null)
            where TComponent : Component
        {
            // создаём через фабрику, чтобы соблюсти жизненный цикл AsSingle()/AsTransient()
            return multiBinder.FromFactory(c =>
            {
                var inst = c.Resolve<IInstantiator>();
                var pos = position ?? Vector3.zero;
                var rot = rotation ?? Quaternion.identity;
                // инстанцируем и возвращаем сам компонент
                return inst.InstantiatePrefabForComponent<TComponent>(prefab, pos, rot, parent);
            });
        }

        /// <summary>
        /// Вариант для Binder&lt;TService&gt; (когда используешь Bind&lt;TService&gt;().To&lt;Impl&gt;()).
        /// Тебе нужно указать, какой компонент вынимать из префаба.
        /// </summary>
        public static DiContainer.Binder<TService> FromComponentInNewPrefab<TService, TComponent>(
            this DiContainer.Binder<TService> binder,
            UnityEngine.Object prefab,
            Vector3? position = null,
            Quaternion? rotation = null,
            Transform parent = null)
            where TComponent : Component
        {
            return binder.FromFactory(c =>
            {
                var inst = c.Resolve<IInstantiator>();
                var pos = position ?? Vector3.zero;
                var rot = rotation ?? Quaternion.identity;
                // NB: возвращаем TService — безопасно, если TComponent : TService
                return (TService)(object)inst.InstantiatePrefabForComponent<TComponent>(prefab, pos, rot, parent);
            });
        }

        /// <summary>
        /// Упрощённый сахар для `FromComponentInNewPrefab<TImpl>` когда TService == TComponent.
        /// То есть: Bind&lt;LoadingController&gt;().ToSelf().FromComponentInNewPrefab(prefab)...
        /// </summary>
        public static DiContainer.Binder<TComponent> FromComponentInNewPrefab<TComponent>(
            this DiContainer.Binder<TComponent> binder,
            UnityEngine.Object prefab,
            Vector3? position = null,
            Quaternion? rotation = null,
            Transform parent = null)
            where TComponent : Component
        {
            return binder.FromFactory(c =>
            {
                var inst = c.Resolve<IInstantiator>();
                var pos = position ?? Vector3.zero;
                var rot = rotation ?? Quaternion.identity;
                return inst.InstantiatePrefabForComponent<TComponent>(prefab, pos, rot, parent);
            });
        }
    }
}