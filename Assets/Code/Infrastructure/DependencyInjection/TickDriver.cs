using UnityEngine;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.DependencyInjection
{
    // Поздний апдейт, чтобы всё остальное уже успело инициализироваться
    [DefaultExecutionOrder(10000)]
    public sealed class TickDriver : MonoBehaviour
    {
        private ITickable _tickable; // здесь будет GameStateMachine

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            TryResolve();
        }

        private void Update()
        {
            if (_tickable == null)
                TryResolve();

            _tickable?.Tick();
        }

        private void TryResolve()
        {
            if (!DiContext.HasInstance) return;

            var c = DiContext.Instance.Container;
            try
            {
                // GSM забинден как AsSingle и реализует ITickable
                var gsm = c.Resolve<IGameStateMachine>();
                _tickable = gsm as ITickable;
            }
            catch
            {
                // контейнер ещё не готов — попробуем на следующем кадре
            }
        }
    }
}