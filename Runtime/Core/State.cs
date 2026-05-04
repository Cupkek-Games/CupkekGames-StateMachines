namespace CupkekGames.StateMachines
{
  public class State
  {
    internal StateSO _originSO;
    internal StateMachine _stateMachine;
    internal StateTransition[] _transitions;
    internal IStateAction[] _actions;

    internal State() { }

    public State(
      StateSO originSO,
      StateMachine stateMachine,
      StateTransition[] transitions,
      IStateAction[] actions)
    {
      _originSO = originSO;
      _stateMachine = stateMachine;
      _transitions = transitions;
      _actions = actions;
    }

    public void OnStateEnter()
    {
      for (int i = 0; i < _transitions.Length; i++)
        _transitions[i].OnStateEnter();
      for (int i = 0; i < _actions.Length; i++)
        _actions[i].OnStateEnter();
    }

    public void OnUpdate()
    {
      for (int i = 0; i < _actions.Length; i++)
        _actions[i].OnUpdate();
    }

    public void OnStateExit()
    {
      for (int i = 0; i < _transitions.Length; i++)
        _transitions[i].OnStateExit();
      for (int i = 0; i < _actions.Length; i++)
        _actions[i].OnStateExit();
    }

    public bool TryGetTransition(out State state)
    {
      state = null;

      for (int i = 0; i < _transitions.Length; i++)
        if (_transitions[i].TryGetTransiton(out state))
          break;

      for (int i = 0; i < _transitions.Length; i++)
        _transitions[i].ClearConditionsCache();

      return state != null;
    }
  }
}
