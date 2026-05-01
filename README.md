# CupkekGames StateMachines

ScriptableObject-driven state machine framework. States and transitions are authored as `TransitionTableSO` assets; an editor window provides authoring + debugging.

## What's inside

**Runtime** (`CupkekGames.StateMachines.asmdef`)

- `StateMachine` (MonoBehaviour) — drives state transitions
- `State` / `StateAction` / `StateCondition` / `StateTransition` — core building blocks
- `IStateComponent` — pluggable component contract
- `StateMachineUpdateMode` — when the FSM ticks
- `StateActionSO` / `StateConditionSO` / `StateSO` / `TransitionTableSO` — ScriptableObject authoring layer
- `StateMachineActionTryTransitionSO` — try-transition action SO
- `StateMachineDebugger` (internal) — runtime debugger
- `InitOnlyAttribute` — utility attribute

**Editor** (`CupkekGames.StateMachines.Editor.asmdef`)

- `StateMachineEditor` / `StateEditor` — inspector tooling
- `TransitionTableEditor` + `TransitionTableEditorWindow` — graph-style transition authoring
- `ScriptTemplates` — quick-create templates for new actions / conditions
- Utilities: `AddTransitionHelper`, `ContentStyle`, `InitOnlyAttributeDrawer`, `SerializedTransition`, `TransitionDisplayHelper`

## Asmdef + namespace

`CupkekGames.StateMachines` (runtime), `CupkekGames.StateMachines.Editor` (editor). Pluralized from the original `CupkekGames.StateMachine` to avoid the namespace = class collision (`class StateMachine`).

## Dependencies

None.

## Repository

[Cupkek-Games/CupkekGames-StateMachines](https://github.com/Cupkek-Games/CupkekGames-StateMachines)
