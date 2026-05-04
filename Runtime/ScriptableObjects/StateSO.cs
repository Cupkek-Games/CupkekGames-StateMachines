using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.StateMachines
{
  [CreateAssetMenu(fileName = "New State", menuName = "CupkekGames/State Machines/State")]
  public class StateSO : ScriptableObject
  {
    [SerializeReference] private List<IStateAction> _actions = new List<IStateAction>();

    /// <summary>
    /// Will create a new state or return an existing one inside <paramref name="createdStates"/>.
    /// </summary>
    internal State GetState(StateMachine stateMachine, Dictionary<StateSO, State> createdStates)
    {
      if (createdStates.TryGetValue(this, out State existing))
        return existing;

      State state = new State();
      createdStates.Add(this, state);

      state._originSO = this;
      state._stateMachine = stateMachine;
      state._transitions = new StateTransition[0];
      state._actions = BuildActions(stateMachine);

      return state;
    }

    private IStateAction[] BuildActions(StateMachine stateMachine)
    {
      int count = _actions.Count;
      IStateAction[] actions = new IStateAction[count];
      for (int i = 0; i < count; i++)
      {
        IStateAction template = _actions[i];
        if (template == null)
        {
          Debug.LogError($"StateSO '{name}' has a null action at index {i}.");
          continue;
        }
        IStateAction instance = template.Clone();
        instance.Awake(stateMachine);
        actions[i] = instance;
      }
      return actions;
    }
  }
}
