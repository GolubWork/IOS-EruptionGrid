// StaticWindow.cs
using UnityEngine;

namespace Code.Windows.StaticWindows
{
    public class StaticWindow : MonoBehaviour
    {
        public StaticWindowId Id { get; protected set; }

        private bool _initialized;
        private bool _subscribed;
        private bool _cleaned;

        private void Awake() => OnAwake();

        private void OnEnable()
        {
            if (!_initialized)
            {
                Initialize();
                _initialized = true;
            }
            if (!_subscribed)
            {
                SubscribeUpdates();
                _subscribed = true;
            }
        }

        private void OnDisable()
        {
            if (_subscribed)
            {
                UnsubscribeUpdates();
                _subscribed = false;
            }
        }

        private void OnDestroy() => Cleanup();

        protected virtual void OnAwake() { }
        protected virtual void Initialize() { }
        protected virtual void SubscribeUpdates() { }
        protected virtual void UnsubscribeUpdates() { }

        protected virtual void Cleanup()
        {
            if (_cleaned) return;
            _cleaned = true;

            if (_subscribed)
            {
                UnsubscribeUpdates();
                _subscribed = false;
            }
        }
    }
}