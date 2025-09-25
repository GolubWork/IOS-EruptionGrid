using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States.StateInfrastructure
{
    public class EndOfFrameExitState : IState, IUpdateable
    {
        private UniTaskCompletionSource  _exitCompletionSource;

        protected bool ExitWasRequested =>
            _exitCompletionSource != null;

        public virtual void Enter()
        {
        }

        UniTask IExitableState.BeginExit()
        {
            _exitCompletionSource = new UniTaskCompletionSource();
            return _exitCompletionSource.Task;
        }

        void IExitableState.EndExit()
        {
            ExitOnEndOfFrame();
            ClearExitPromise();
        }

        void IUpdateable.Update()
        {
            if (!ExitWasRequested)
                OnUpdate();
      
            if (ExitWasRequested) 
                ResolveExitPromise();
        }

        protected virtual void ExitOnEndOfFrame()
        {
      
        }

        protected virtual void OnUpdate(){}

        private void ClearExitPromise() =>
            _exitCompletionSource = null;

        private void ResolveExitPromise() =>
            _exitCompletionSource?.TrySetResult();
    
    }
}