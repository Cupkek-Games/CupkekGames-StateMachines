using System;

namespace CupkekGames.StateMachines
{
  [Serializable]
  public class StateMachineActionTryTransition : StateAction
  {
    private StateMachine _stateMachine;

    public override void Awake(StateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {
      _stateMachine.TryTransition();
    }
  }
}
