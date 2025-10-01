using System;
using Code.Infrastructure.DependencyInjection;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.StateInfrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States.StateMachine
{
  public class GameStateMachine : IGameStateMachine, ITickable
  {
    private IExitableState _activeState;
    private readonly IStateFactory _stateFactory;
    private Type _activeStateType;

    public GameStateMachine(IStateFactory stateFactory) => _stateFactory = stateFactory;

    public void Tick()
    {
      if (_activeState is IUpdateable updateableState)
        updateableState.Update();
    }

    public void Enter<TState>() where TState : class, IState
    {
      Debug.Log($"[GSM] Enter<{typeof(TState).Name}>");
      RequestEnter<TState>().Forget();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      Debug.Log($"[GSM] Enter<{typeof(TState).Name}, {typeof(TPayload).Name}> payload={payload}");
      RequestEnter<TState, TPayload>(payload).Forget();
    }

    public bool CompareState<TState>() where TState : class, IState =>
      _activeStateType == typeof(TState);

    private async UniTask<TState> RequestEnter<TState>() where TState : class, IState
    {
      var state = await RequestChangeState<TState>();
      return EnterState(state);
    }

    private async UniTask<TState> RequestEnter<TState, TPayload>(TPayload payload)
      where TState : class, IPayloadState<TPayload>
    {
      var state = await RequestChangeState<TState>();
      return EnterPayloadState(state, payload);
    }

    private TState EnterState<TState>(TState state) where TState : class, IState
    {
      _activeState = state;
      _activeStateType = typeof(TState);
      Debug.Log($"[GSM] -> calling {typeof(TState).Name}.Enter()");
      state.Enter();
      return state;
    }

    private TState EnterPayloadState<TState, TPayload>(TState state, TPayload payload)
      where TState : class, IPayloadState<TPayload>
    {
      _activeState = state;
      _activeStateType = typeof(TState);
      Debug.Log($"[GSM] -> calling {typeof(TState).Name}.Enter(payload)");
      state.Enter(payload);
      return state;
    }

    private async UniTask<TState> RequestChangeState<TState>() where TState : class, IExitableState
    {
      if (_activeState != null)
      {
        try
        {
          Debug.Log($"[GSM] BeginExit {_activeState.GetType().Name}...");
          // страхуемся от вечного ожидания
          await _activeState.BeginExit().Timeout(TimeSpan.FromSeconds(5));
          Debug.Log($"[GSM] EndExit {_activeState.GetType().Name}");
          _activeState.EndExit();
        }
        catch (Exception e)
        {
          Debug.LogException(e);
        }
      }

      return ChangeState<TState>();
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      Debug.Log($"[GSM] Resolve state {typeof(TState).Name}...");
      TState state = _stateFactory.GetState<TState>();
      Debug.Log($"[GSM] Resolved instance: {state?.GetType().Name ?? "NULL"}");
      return state;
    }
  }
}
