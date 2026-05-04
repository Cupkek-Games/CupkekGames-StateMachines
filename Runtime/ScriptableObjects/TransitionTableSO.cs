using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CupkekGames.StateMachines
{
  [CreateAssetMenu(fileName = "NewTransitionTable", menuName = "CupkekGames/State Machines/Transition Table")]
  public class TransitionTableSO : ScriptableObject
  {
    [SerializeField] private List<TransitionItem> _transitions = new List<TransitionItem>();

    public IReadOnlyList<TransitionItem> Transitions => _transitions;

    /// <summary>
    /// Will get the initial state and instantiate all subsequent states, transitions, actions and conditions.
    /// </summary>
    internal State GetInitialState(StateMachine stateMachine)
    {
      List<State> states = new List<State>();
      List<StateTransition> transitions = new List<StateTransition>();
      Dictionary<StateSO, State> createdStates = new Dictionary<StateSO, State>();

      var fromStates = _transitions.GroupBy(transition => transition.FromState);

      foreach (var fromState in fromStates)
      {
        if (fromState.Key == null)
          throw new ArgumentNullException(nameof(fromState.Key), $"TransitionTable: {name}");

        State state = fromState.Key.GetState(stateMachine, createdStates);
        states.Add(state);

        transitions.Clear();
        foreach (TransitionItem transitionItem in fromState)
        {
          if (transitionItem.ToState == null)
            throw new ArgumentNullException(nameof(transitionItem.ToState), $"TransitionTable: {name}, From State: {fromState.Key.name}");

          State toState = transitionItem.ToState.GetState(stateMachine, createdStates);
          ProcessConditionUsages(stateMachine, transitionItem.Conditions, out StateConditionEvaluation[] conditions, out int[] resultGroups);
          transitions.Add(new StateTransition(toState, conditions, resultGroups));
        }

        state._transitions = transitions.ToArray();
      }

      return states.Count > 0 ? states[0]
        : throw new InvalidOperationException($"TransitionTable {name} is empty.");
    }

    private static void ProcessConditionUsages(
      StateMachine stateMachine,
      List<ConditionUsage> conditionUsages,
      out StateConditionEvaluation[] conditions,
      out int[] resultGroups)
    {
      int count = conditionUsages.Count;
      conditions = new StateConditionEvaluation[count];
      for (int i = 0; i < count; i++)
      {
        IStateCondition template = conditionUsages[i].Condition;
        if (template == null)
        {
          Debug.LogError($"TransitionTableSO has a null condition at index {i}.");
          continue;
        }
        IStateCondition instance = template.Clone();
        instance.Awake(stateMachine);
        conditions[i] = new StateConditionEvaluation(stateMachine, instance, conditionUsages[i].ExpectedResult == Result.True);
      }

      List<int> resultGroupsList = new List<int>();
      for (int i = 0; i < count; i++)
      {
        int idx = resultGroupsList.Count;
        resultGroupsList.Add(1);
        while (i < count - 1 && conditionUsages[i].Operator == Operator.And)
        {
          i++;
          resultGroupsList[idx]++;
        }
      }

      resultGroups = resultGroupsList.ToArray();
    }

    [Serializable]
    public class TransitionItem
    {
      public StateSO FromState;
      public StateSO ToState;
      public List<ConditionUsage> Conditions = new List<ConditionUsage>();
    }

    [Serializable]
    public class ConditionUsage
    {
      public Result ExpectedResult;
      [SerializeReference] public IStateCondition Condition;
      public Operator Operator;
    }

    public enum Result { True, False }
    public enum Operator { And, Or }
  }
}
