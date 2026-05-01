using UnityEngine;

namespace CupkekGames.StateMachines
{
  [CreateAssetMenu(fileName = "TryTransition", menuName = "CupkekGames/State Machines/Action/TryTransition")]
  public class StateMachineActionTryTransitionSO : StateActionSO
  {
    protected override StateAction CreateAction() => new StateMachineActionTryTransition();
  }

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