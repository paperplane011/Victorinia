#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomEditor(typeof(Question))]
[CanEditMultipleObjects]
public class QuestionInspector : Editor
{
    private const int MIN_NUM_OF_QUESTIONS = 2;
    private const int MAX_NUM_OF_QUESTIONS = 5;

    SerializedProperty _questionTextProperty;
    SerializedProperty _answerItemArrayProperty;
    SerializedProperty _questionDifficulty;
    SerializedProperty _tag;

    private void OnEnable()
    {
        _answerItemArrayProperty = serializedObject.FindProperty("_answerArray");
        _questionTextProperty = serializedObject.FindProperty("_questionText");
        _questionDifficulty = serializedObject.FindProperty("_questionDifficulty");
        _tag = serializedObject.FindProperty("_tag");

    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_questionDifficulty);
        EditorGUILayout.PropertyField(_tag);
        EditorGUILayout.PropertyField(_questionTextProperty);
        EditorGUILayout.PropertyField(_answerItemArrayProperty);
        


        _answerItemArrayProperty.arraySize = Mathf.Clamp(_answerItemArrayProperty.arraySize, MIN_NUM_OF_QUESTIONS, MAX_NUM_OF_QUESTIONS);

        int correctAnswersNum = 0;
        bool isAnyAnswerEmpty = false;

        for (int i = 0; i < _answerItemArrayProperty.arraySize; i++)
        {
            SerializedProperty answerItemProperty = _answerItemArrayProperty.GetArrayElementAtIndex(i);

            bool isCorrect = answerItemProperty.FindPropertyRelative("_isCorrect").boolValue;
            if (isCorrect) correctAnswersNum++;

            string answerText = answerItemProperty.FindPropertyRelative("_answerText").stringValue.Trim(' ');
            if (answerText == "") isAnyAnswerEmpty = true;

        }


       


        EditorGUILayout.LabelField($"Num of correct answers: {correctAnswersNum}");

        


        

        if (isAnyAnswerEmpty)
        {
            EditorGUILayout.LabelField("Some answer text is empty");
        }



        QuestionPreview();

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }



    private void QuestionPreview()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("--- QUESTION PREVIEW ---");
        EditorGUILayout.Space();

        EditorGUILayout.LabelField(_questionTextProperty.stringValue);
        EditorGUILayout.Space();

        for (int i = 0; i < _answerItemArrayProperty.arraySize; i++)
        {
            SerializedProperty answerItemProperty = _answerItemArrayProperty.GetArrayElementAtIndex(i);
            bool isCorrect = answerItemProperty.FindPropertyRelative("_isCorrect").boolValue;

            string answerText = answerItemProperty.FindPropertyRelative("_answerText").stringValue;

            EditorGUILayout.LabelField(" - " + answerText + ((isCorrect) ? "  ✓" : ""));


        }
    }


    
}
