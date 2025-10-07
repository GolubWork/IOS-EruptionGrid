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
      RequestEnter<TState>().Forget();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
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
      state.Enter();
      return state;
    }

    private TState EnterPayloadState<TState, TPayload>(TState state, TPayload payload)
      where TState : class, IPayloadState<TPayload>
    {
      _activeState = state;
      _activeStateType = typeof(TState);
      state.Enter(payload);
      return state;
    }

    private async UniTask<TState> RequestChangeState<TState>() where TState : class, IExitableState
    {
      if (_activeState != null)
      {
        try
        {
          await _activeState.BeginExit().Timeout(TimeSpan.FromSeconds(5));
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
      TState state = _stateFactory.GetState<TState>();
      return state;
    }
  }
}
