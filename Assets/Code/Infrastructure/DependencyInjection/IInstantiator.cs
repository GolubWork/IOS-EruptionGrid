using UnityEngine;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Инстанцирует префабы и сразу делает DI-инъекцию всего дерева (+ Initialize/LateInitialize, если подключено).
    /// Удобные перегрузки для обычных объектов и UI.
    /// </summary>
    public interface IInstantiator
    {
        // ---- GameObject ----
        GameObject InstantiatePrefab(UnityEngine.Object prefab);
        GameObject InstantiatePrefab(UnityEngine.Object prefab, Transform parent, bool worldPositionStays = false);
        GameObject InstantiatePrefab(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parent = null);

        // ---- Component ----
        T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab) where T : Component;
        T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Transform parent, bool worldPositionStays = false) where T : Component;
        T InstantiatePrefabForComponent<T>(UnityEngine.Object prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component;
    }
}