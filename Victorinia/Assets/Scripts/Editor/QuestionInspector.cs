using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Question))]
[CanEditMultipleObjects]
public class QuestionInspector : Editor
{
    private const int MIN_NUM_OF_QUESTIONS = 2;
    private const int MAX_NUM_OF_QUESTIONS = 6;

    SerializedProperty _questionTextProperty;
    SerializedProperty _answerItemArrayProperty;
    SerializedProperty _isMultipleAnswers;
    SerializedProperty _questionDifficulty;

    private void OnEnable()
    {
        _answerItemArrayProperty = serializedObject.FindProperty("_answerArray");
        _questionTextProperty = serializedObject.FindProperty("_questionText");
        _isMultipleAnswers = serializedObject.FindProperty("_isMultipleAnswers");
        _questionDifficulty = serializedObject.FindProperty("_questionDifficulty");

    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_questionDifficulty);
        EditorGUILayout.PropertyField(_questionTextProperty);
        EditorGUILayout.PropertyField(_isMultipleAnswers);
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


        if(_isMultipleAnswers.boolValue)
        {
            EditorGUILayout.LabelField("MULTIPLE ANSWERS ENABLED");
        }
        else
        {
            EditorGUILayout.LabelField("MULTIPLE ANSWERS DISABLED");
        }


        EditorGUILayout.LabelField($"Num of correct answers: {correctAnswersNum}");

        


        EditorGUILayout.LabelField($"CheckCorrectness: {CheckCorrectness(_isMultipleAnswers.boolValue, correctAnswersNum)}");

        if (isAnyAnswerEmpty)
        {
            EditorGUILayout.LabelField("Some answer text is empty");
        }



        QuestionPreview();

        serializedObject.ApplyModifiedProperties();
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


    private bool CheckCorrectness(bool isMultipleAnswers, int numOfCorrectAnsws)
    {
        if (numOfCorrectAnsws == 0) return false;

        if (isMultipleAnswers && numOfCorrectAnsws == 1) return false;

        if (!isMultipleAnswers && numOfCorrectAnsws > 1) return false;

        return true;
    }
}
