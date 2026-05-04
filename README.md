# CupkekGames StateMachines

ScriptableObject-authored state machine framework. States and transitions are authored as
`StateSO` / `TransitionTableSO` assets; actions and conditions are inlined on those assets
as `[SerializeReference]` polymorphic data — no per-leaf factory ScriptableObjects.

## What's inside

**Runtime** (`CupkekGames.StateMachines.asmdef`)

- `StateMachine` (MonoBehaviour) — drives state transitions
- `State` / `StateTransition` / `StateConditionEvaluation` — runtime building blocks
- `IStateAction` / `IStateCondition` — authoring contracts (implement on plain `[Serializable]` classes)
- `StateAction` / `Condition` — convenience base classes with virtual no-op overrides
- `StateMachineActionTryTransition` — built-in action that pumps `StateMachine.TryTransition`
- `StateMachineUpdateMode` — when the FSM ticks
- `StateSO` — list of `[SerializeReference] IStateAction`
- `TransitionTableSO` — `TransitionItem` + `ConditionUsage` (`[SerializeReference] IStateCondition`)
- `StateMachineDebugger` (internal) — runtime debugger

**Editor** (`CupkekGames.StateMachines.Editor.asmdef`)

- `StateMachineCustomEditor` — debug start button on the StateMachine MonoBehaviour

State and transition table assets use Unity's default inspector: `[SerializeReference]`
gives you the polymorphic-class picker for actions and conditions out of the box.

## Asmdef + namespace

`CupkekGames.StateMachines` (runtime), `CupkekGames.StateMachines.Editor` (editor).
Pluralized from the original `CupkekGames.StateMachine` to avoid the namespace = class
collision (`class StateMachine`).

## Dependencies

None.

## Repository

[Cupkek-Games/CupkekGames-StateMachines](https://github.com/Cupkek-Games/CupkekGames-StateMachines)
