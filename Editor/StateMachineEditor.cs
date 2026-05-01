#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CupkekGames.StateMachines.Editor
{
    [CustomEditor(typeof(StateMachine), true)]
    public class StateMachineCustomEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            // Create the root element
            var root = new VisualElement();

            // Add the default inspector
            var defaultInspector = new IMGUIContainer(() => DrawDefaultInspector());
            root.Add(defaultInspector);

            Label title = new Label("StateMachine Debug");
            title.style.unityTextAlign = TextAnchor.MiddleCenter;
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            title.style.fontSize = 24f;
            root.Add(title);

            // Add buttons
            Button button = new Button(() =>
            {
                StateMachine stateMachine = (StateMachine)target;
                stateMachine.StartStateMachine();
            })
            {
                text = "Start"
            };
            root.Add(button);

            return root;
        }
    }
}
#endif