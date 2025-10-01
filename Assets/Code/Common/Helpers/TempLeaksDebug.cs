#if ENABLE_UNITY_COLLECTIONS_CHECKS
using Unity.Collections;
using UnityEngine;

public static class TempLeaksDebug
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnableLeakStacks()
    {
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
    }
}
#endif