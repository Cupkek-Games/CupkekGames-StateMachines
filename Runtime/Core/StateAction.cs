using System;

namespace CupkekGames.StateMachines
{
  /// <summary>
  /// Base class for state actions. Subclasses author behavior; serialized fields are
  /// authored once on the owning <see cref="StateSO"/> via <c>[SerializeReference]</c>.
  /// Default <see cref="Clone"/> returns a new instance via the parameterless constructor —
  /// override if your action carries authored configuration.
  /// </summary>
  [Serializable]
  public abstract class StateAction : IStateAction
  {
    public virtual void Awake(StateMachine stateMachine) { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void OnUpdate() { }

    public virtual IStateAction Clone() => (IStateAction)Activator.CreateInstance(GetType());

    /// <summary>
    /// This enum is used to create flexible <c>StateActions</c> which can execute in any of the 3 available "moments".
    /// The StateAction in this case would have to implement all the relative functions, and use an if statement with this enum as a condition to decide whether to act or not in each moment.
    /// </summary>
    public enum SpecificMoment
    {
      OnStateEnter, OnStateExit, OnUpdate,
    }
  }
}
