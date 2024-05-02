
using UnityEditor;


[CustomEditor(typeof(PlayerEventsInvokerButton))]
[CanEditMultipleObjects]
public class PlayerEventsInvokerButtonInspector : Editor
{
    SerializedProperty _eventToInvokeProperty;
    SerializedProperty _hasArgsProperty;
    SerializedProperty _boolArgProperty;

    private void OnEnable()
    {
        _eventToInvokeProperty = serializedObject.FindProperty("_eventToInvoke");
        _hasArgsProperty = serializedObject.FindProperty("_hasArgs");
        _boolArgProperty = serializedObject.FindProperty("_boolArg");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_eventToInvokeProperty);

        if (_eventToInvokeProperty.enumValueIndex == (int)PlayerEventBus.EventType.OnAnswerPressed)
        {
            _hasArgsProperty.boolValue = true;
            EditorGUILayout.PropertyField(_boolArgProperty);
        }
        else
        {
            _hasArgsProperty.boolValue = false;
        }


        serializedObject.ApplyModifiedProperties();
    }

}
