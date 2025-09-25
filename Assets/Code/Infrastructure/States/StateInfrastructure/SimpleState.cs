using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.StateInfrastructure
{
    public class SimpleState : IState
    {
        public virtual void Enter()
        {
        }

        protected virtual void Exit()
        {
        }

        async UniTask IExitableState.BeginExit()
        {
            Exit();
            await UniTask.CompletedTask;
        }

        void IExitableState.EndExit()
        {
        }
    }
}