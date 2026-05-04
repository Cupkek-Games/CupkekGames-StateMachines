using System;

namespace CupkekGames.StateMachines
{
  /// <summary>
  /// Base class for transition conditions. Subclasses override <see cref="Statement"/>;
  /// serialized fields are authored once on the owning <see cref="TransitionTableSO"/>
  /// via <c>[SerializeReference]</c>. Default <see cref="Clone"/> returns a new instance via
  /// the parameterless constructor — override if your condition carries authored configuration.
  /// </summary>
  [Serializable]
  public abstract class Condition : IStateCondition
  {
    public virtual void Awake(StateMachine stateMachine) { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public abstract bool Statement();

    public virtual IStateCondition Clone() => (IStateCondition)Activator.CreateInstance(GetType());
  }

  /// <summary>
  /// Runtime evaluation wrapper that pairs an <see cref="IStateCondition"/> with the expected
  /// boolean outcome and caches its <see cref="IStateCondition.Statement"/> result for the
  /// current evaluation pass.
  /// </summary>
  public class StateConditionEvaluation
  {
    private readonly StateMachine _stateMachine;
    private readonly IStateCondition _condition;
    private readonly bool _expectedResult;
    private bool _isCached;
    private bool _cachedStatement;

    public IStateCondition Condition => _condition;

    public StateConditionEvaluation(StateMachine stateMachine, IStateCondition condition, bool expectedResult)
    {
      _stateMachine = stateMachine;
      _condition = condition;
      _expectedResult = expectedResult;
    }

    public bool IsMet()
    {
      if (!_isCached)
      {
        _isCached = true;
        _cachedStatement = _condition.Statement();
      }

      bool isMet = _cachedStatement == _expectedResult;

#if UNITY_EDITOR
      _stateMachine._debugger.TransitionConditionResult(_condition.GetType().Name, _cachedStatement, isMet);
#endif
      return isMet;
    }

    public void OnStateEnter() => _condition.OnStateEnter();
    public void OnStateExit() => _condition.OnStateExit();
    public void ClearCache() => _isCached = false;
  }
}
