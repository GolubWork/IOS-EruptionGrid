using UnityEngine;

namespace Code.Infrastructure.DependencyInjection
{
    // Чуть позже MonoInstaller, но раньше большинства скриптов
    [DefaultExecutionOrder(-31999)]
    public sealed class KeepAlive : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}