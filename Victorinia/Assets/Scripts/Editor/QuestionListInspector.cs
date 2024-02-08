using System;
using UnityEditor;

[CustomEditor(typeof(QuestionList))]
public class QuestionListInspector : Editor
{

    SerializedProperty _thisQuestionListDifficulty;
    SerializedProperty _thisQuestionList;

   

    private void OnEnable()
    {
        _thisQuestionListDifficulty = serializedObject.FindProperty("_thisQuestionListDifficulty");
        _thisQuestionList = serializedObject.FindProperty("_thisQuestionList");

    }

 

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_thisQuestionListDifficulty);


        EditorGUI.BeginDisabledGroup(true);


        EditorGUILayout.PropertyField(_thisQuestionList);
        Repaint();


        EditorGUI.EndDisabledGroup();


        


        serializedObject.ApplyModifiedProperties();
    }



    


}
