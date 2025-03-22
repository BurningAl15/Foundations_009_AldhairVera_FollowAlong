using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorTrigger))]
public class DoorTriggerEditor : Editor
{
    SerializedProperty door;
    SerializedProperty doorAnimation;
    SerializedProperty animationCurve;
    SerializedProperty animationDuration;
    SerializedProperty rotationTarget;
    SerializedProperty upPositionOffset;

    void OnEnable()
    {
        door = serializedObject.FindProperty("_door");
        doorAnimation = serializedObject.FindProperty("_doorAnimation");
        animationCurve = serializedObject.FindProperty("_animationCurve");
        animationDuration = serializedObject.FindProperty("_animationDuration");
        rotationTarget = serializedObject.FindProperty("_rotationTarget");
        upPositionOffset = serializedObject.FindProperty("_upPositionOffset");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(door);
        EditorGUILayout.PropertyField(doorAnimation);
        EditorGUILayout.PropertyField(animationCurve);
        EditorGUILayout.PropertyField(animationDuration);

        DoorAnimation animType = (DoorAnimation)doorAnimation.enumValueIndex;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Specific Animation Variables", EditorStyles.boldLabel);

        switch (animType)
        {
            case DoorAnimation.RotateDoor:
                EditorGUILayout.PropertyField(rotationTarget, new GUIContent("Rotation Target"));
                break;
            case DoorAnimation.UpDoor:
                EditorGUILayout.PropertyField(upPositionOffset, new GUIContent("Up Position Offset"));
                break;
            case DoorAnimation.DisappearDoor:
                EditorGUILayout.HelpBox("No extra variables needed for DisappearDoor.", MessageType.Info);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

