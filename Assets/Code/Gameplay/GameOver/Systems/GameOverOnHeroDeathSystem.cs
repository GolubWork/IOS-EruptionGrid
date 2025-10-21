using System.Collections;
using System.Collections.Generic;
using Code.Audios.Audio;
using Code.Audios.Audio.Factory;
using Code.Infrastructure;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.GameOver.Systems
{
  public class GameOverOnHeroDeathSystem : ReactiveSystem<GameEntity>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IAudioFactory _audioFactory;

    public GameOverOnHeroDeathSystem(GameContext game, 
      IGameStateMachine stateMachine, 
      ICoroutineRunner coroutineRunner,
      IAudioFactory audioFactory) : base(game)
    {
      _stateMachine = stateMachine;
      _coroutineRunner = coroutineRunner;
      _audioFactory = audioFactory;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
      context.CreateCollector(GameMatcher
        .AllOf(
          GameMatcher.Player,
          GameMatcher.Dead)
        .Added());

    protected override bool Filter(GameEntity chicken) => chicken.isDead;

    protected override void Execute(List<GameEntity> chickens)
    {
      foreach (GameEntity chicken in chickens)
      {
        chicken.isDestructed = true;
      }
      _coroutineRunner.StartCoroutine(GameOverCor());
    }

    private IEnumerator GameOverCor()
    {
      _audioFactory.CreateSound(SoundTypeId.Collider);
      _audioFactory.CreateSound(SoundTypeId.Collider2);
      yield return new WaitForSeconds(1f);
      _stateMachine.Enter<GameOverState>();
    }
    
  }
}