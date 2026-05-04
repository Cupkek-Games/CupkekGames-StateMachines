namespace CupkekGames.StateMachines
{
  public interface IStateAction
  {
    void Awake(StateMachine stateMachine);
    void OnStateEnter();
    void OnStateExit();
    void OnUpdate();
    IStateAction Clone();
  }
}
