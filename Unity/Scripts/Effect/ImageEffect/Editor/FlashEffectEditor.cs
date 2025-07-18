using UnityEditor;
using UnityEngine;

namespace Tea.UIEffect
{
    [CustomEditor(typeof(FlashEffect))]
    public class FlashEffectEditor : Editor
    {
        // public override VisualElement CreateInspectorGUI()
        // {
        //     throw new System.NotImplementedException();
        // }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            FlashEffect flash = (FlashEffect)target;
            if (GUILayout.Button("SwitchToFlash"))
            {
                flash.RefreshColor();
                flash.SwitchToFlash();
                EditorUtility.SetDirty(target);
                Repaint();
            }

            if (GUILayout.Button("SwitchToNormal"))
            {
                flash.RefreshColor();
                flash.SwitchToNormal();
                EditorUtility.SetDirty(target);
                Repaint();
            }

            if (GUILayout.Button("RefreshColor"))
            {
                flash.RefreshColor();
                EditorUtility.SetDirty(target);
                Repaint();
            }

            if (GUILayout.Button("Clear"))
            {
                flash.Clear();
                EditorUtility.SetDirty(target);
                Repaint();
            }
            // EditorGUI.BeginChangeCheck();

            // SerializedProperty flashColor = serializedObject.FindProperty("_flashColor");
            // // EditorGUILayout.PropertyField(flashColor);

            // if (EditorGUI.EndChangeCheck())
            // {
            //     serializedObject.ApplyModifiedProperties();

            //     flash.RefreshColor();
            // }
        }
    }
}
