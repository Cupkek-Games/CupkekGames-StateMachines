namespace CupkekGames.StateMachines
{
  public interface IStateCondition
  {
    void Awake(StateMachine stateMachine);
    void OnStateEnter();
    void OnStateExit();
    bool Statement();
    IStateCondition Clone();
  }
}
