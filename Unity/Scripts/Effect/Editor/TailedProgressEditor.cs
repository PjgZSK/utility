using UnityEditor;
using UnityEngine;

namespace Tea.UIEffect
{
    [CustomEditor(typeof(TailedProgress))]
    public class TailedProgressEditor : Editor
    {
        // public override VisualElement CreateInspectorGUI()
        // {
        //     throw new System.NotImplementedException();
        // }
        private SerializedProperty fillAmount;

        private void OnEnable()
        {
            fillAmount = serializedObject.FindProperty("_fillAmount");
        }

        public override void OnInspectorGUI()
        {
            // DrawDefaultInspector();
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(fillAmount, new GUIContent("FA"));

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                TailedProgress progress = (TailedProgress)target;
                progress.Init();
                progress.TriggerFillAmountChange();
            }

            if (GUILayout.Button("Reset"))
            {
                TailedProgress progress = (TailedProgress)target;
                progress.Reset();
            }
            DrawPropertiesExcluding(serializedObject, "_fillAmount");
            serializedObject.ApplyModifiedProperties();
        }
    }
}
