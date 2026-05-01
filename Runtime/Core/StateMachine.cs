using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CupkekGames.StateMachines
{
  public class StateMachine : MonoBehaviour
  {
    [Tooltip("Set the initial state of this StateMachine")]
    [SerializeField] private TransitionTableSO _transitionTableSO;

#if UNITY_EDITOR
    [Space]
    [SerializeField]
    internal Debugging.StateMachineDebugger _debugger;
#endif

    private readonly Dictionary<Type, Component> _cachedComponents = new Dictionary<Type, Component>();
    internal State _currentState;
    [SerializeField] private StateMachineUpdateMode _updateMode;
    [SerializeField] private bool _useUpdate;
    private bool _isRunning;
    public bool IsRunning => _isRunning;

    public virtual void Awake()
    {
      _currentState = _transitionTableSO.GetInitialState(this);

#if UNITY_EDITOR
      _debugger.Awake(this);
#endif
    }

    public virtual void OnEnable()
    {
#if UNITY_EDITOR
      UnityEditor.AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
#endif
    }

#if UNITY_EDITOR
    private void OnAfterAssemblyReload()
    {
      _currentState = _transitionTableSO.GetInitialState(this);
      _debugger.Awake(this);
    }
#endif

    public virtual void OnDisable()
    {
#if UNITY_EDITOR
      UnityEditor.AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
#endif
    }

    public virtual void StartStateMachine()
    {
      if (_isRunning)
        return;

      _currentState.OnStateEnter();

      if (_updateMode == StateMachineUpdateMode.Update)
        StartCoroutine(TransitionCoroutine());

      if (_useUpdate)
        StartCoroutine(UseUpdateCoroutine());
    }

    public new bool TryGetComponent<T>(out T component) where T : Component
    {
      var type = typeof(T);
      if (!_cachedComponents.TryGetValue(type, out var value))
      {
        if (base.TryGetComponent<T>(out component))
          _cachedComponents.Add(type, component);

        return component != null;
      }

      component = (T)value;
      return true;
    }

    public T GetOrAddComponent<T>() where T : Component
    {
      if (!TryGetComponent<T>(out var component))
      {
        component = gameObject.AddComponent<T>();
        _cachedComponents.Add(typeof(T), component);
      }

      return component;
    }

    public new T GetComponent<T>() where T : Component
    {
      return TryGetComponent(out T component)
        ? component : throw new InvalidOperationException($"{typeof(T).Name} not found in {name}.");
    }

    private IEnumerator TransitionCoroutine()
    {
      while (true)
      {
        if (_currentState.TryGetTransition(out var transitionState))
          Transition(transitionState);
        yield return null;
      }
    }

    private IEnumerator UseUpdateCoroutine()
    {
      while (true)
      {
        _currentState.OnUpdate();
        yield return null;
      }
    }

    private void Transition(State transitionState)
    {
      _currentState.OnStateExit();
      _currentState = transitionState;
      _currentState.OnStateEnter();
    }

    /// <summary>
    /// Call this method to manually transition to the next state.
    /// </summary>
    public void TryTransition()
    {
      if (_currentState.TryGetTransition(out var transitionState))
        Transition(transitionState);
    }
  }
}
