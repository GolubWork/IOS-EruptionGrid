using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Реализация поверх Unity Object.Instantiate с авто-инъекцией через MiniDi.DiContext.
    /// </summary>
    /// <summary>
    /// Реализация через UnityEngine.Object.Instantiate + DiContext.InjectGameObject.
    /// </summary>
    public sealed class UnityDiInstantiator : IInstantiator
    {
        private static GameObject _stageRoot; // общий неактивный контейнер

        private static Transform GetInactiveStage()
        {
            if (_stageRoot == null)
            {
                _stageRoot = new GameObject("[DI_Stage_Inactive]");
                _stageRoot.SetActive(false);                 // критично: неактивен
                Object.DontDestroyOnLoad(_stageRoot);
            }
            return _stageRoot.transform;
        }

        // -------- GameObject --------
        public GameObject InstantiatePrefab(Object prefab, Transform parent, bool worldPositionStays = false)
        {
            var goPrefab = GetPrefabRootOrThrow(prefab);

            // инстансим под неактивным родителем -> Awake вызовется, OnEnable — НЕТ
            var stage = GetInactiveStage();
            var instance = Object.Instantiate(goPrefab, stage, false);

            PostInject(instance); // DI + Initialize

            // теперь перевешиваем к целевому родителю и активируем
            instance.transform.SetParent(parent, worldPositionStays);
            instance.SetActive(true);
            return instance;
        }

        public GameObject InstantiatePrefab(Object prefab)
        {
            var goPrefab = GetPrefabRootOrThrow(prefab);

            var stage = GetInactiveStage();
            var instance = Object.Instantiate(goPrefab, stage, false);

            PostInject(instance);

            instance.transform.SetParent(null, false);
            instance.SetActive(true);
            return instance;
        }

        public GameObject InstantiatePrefab(Object prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var goPrefab = GetPrefabRootOrThrow(prefab);

            var stage = GetInactiveStage();
            var instance = Object.Instantiate(goPrefab, stage, false);

            PostInject(instance);

            instance.transform.SetParent(parent, false);
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.SetActive(true);
            return instance;
        }

        // -------- Component --------
        public T InstantiatePrefabForComponent<T>(Object prefab, Transform parent, bool worldPositionStays = false) where T : Component
        {
            var go = InstantiatePrefab(prefab, parent, worldPositionStays); // уже с буфером и DI
            return GetComponentOrThrow<T>(prefab, go);
        }

        public T InstantiatePrefabForComponent<T>(Object prefab) where T : Component
        {
            var go = InstantiatePrefab(prefab);
            return GetComponentOrThrow<T>(prefab, go);
        }

        public T InstantiatePrefabForComponent<T>(Object prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
        {
            var go = InstantiatePrefab(prefab, position, rotation, parent);
            return GetComponentOrThrow<T>(prefab, go);
        }

        // ---- helpers (как у тебя) ----
        private static GameObject GetPrefabRootOrThrow(UnityEngine.Object prefab)
        {
            if (prefab is GameObject go) return go;
            if (prefab is Component c && c != null) return c.gameObject;
            throw new ArgumentException($"[UnityDiInstantiator] Prefab must be GameObject or Component. Got: {prefab?.GetType().Name}", nameof(prefab));
        }

        static T GetComponentOrThrow<T>(UnityEngine.Object prefab, GameObject instance) where T : Component
        {
            var comp = instance.GetComponentInChildren<T>(includeInactive: true);
            if (comp == null)
                throw new InvalidOperationException(
                    $"[UnityDiInstantiator] Component '{typeof(T).Name}' not found on instantiated prefab '{prefab?.name}'.");
            return comp;
        }

        private static void PostInject(GameObject instance)
        {
            var ctx = DiContext.Instance
                      ?? throw new System.InvalidOperationException("[UnityDiInstantiator] DiContext.Instance is null.");

            // DI для всего дерева
            ctx.InjectGameObject(instance, includeChildren: true);

            // запуски IInitializable/ILateInitializable (если используешь)
            try { InitializableUtility.RunOnGameObject(instance, includeChildren: true); } catch { }
        }
    }
}