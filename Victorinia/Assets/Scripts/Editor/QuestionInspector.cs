using UnityEditor;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

[CustomEditor(typeof(Question))]
public class QuestionInspector : Editor
{

    private const int MIN_NUM_OF_QUESTIONS = 2;
    private const int MAX_NUM_OF_QUESTIONS = 6;

    SerializedProperty _questionTextProperty;
    SerializedProperty _answerItemArrayProperty;


    private void OnEnable()
    {
        _answerItemArrayProperty = serializedObject.FindProperty("_answerItemArray");
        _questionTextProperty = serializedObject.FindProperty("_questionText");

    }

    public override void OnInspectorGUI()
    {
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
    }



    private void QuestionPreview()
    {
        EditorGUILayout.Space();
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
