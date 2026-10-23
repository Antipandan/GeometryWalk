using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(FollowTarget))]
public class CustomEditorFollowTarget : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FollowTarget followTarget = (FollowTarget)target;
        if (GUILayout.Button("Generate offset"))
        {
            followTarget.CalculateOffset();
        }
        EditorGUILayout.Space();
    }
}