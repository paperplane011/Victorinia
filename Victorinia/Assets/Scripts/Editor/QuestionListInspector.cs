using System;
using UnityEditor;
using UnityEngine;

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
      
        EditorGUI.EndDisabledGroup();


        serializedObject.ApplyModifiedProperties();
    }



    


}
