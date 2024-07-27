#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(Topic))]
[CanEditMultipleObjects]
public class TopicInspector : Editor
{
    SerializedProperty _tag;

    SerializedProperty _previewSpriteProperty;
    SerializedProperty _captionProperty;

    SerializedProperty _questionDifficultyToQuestionListArrayProperty;
    SerializedProperty _questionDifficultyToRewardArrayProperty;

    SerializedProperty _questionDifficultyToLockedStatusArrayProperty;
    SerializedProperty _questionDifficultyToCompletedStatusArrayProperty;
    SerializedProperty _questionDifficultyToCostArrayProperty;


    private void OnEnable()
    {
        _tag = serializedObject.FindProperty("_tag");


        _previewSpriteProperty = serializedObject.FindProperty("_previewSprite");
        _captionProperty = serializedObject.FindProperty("_caption");

        _questionDifficultyToQuestionListArrayProperty = serializedObject.FindProperty("_questionDifficultyToQuestionListArray");
        _questionDifficultyToRewardArrayProperty = serializedObject.FindProperty("_questionDifficultyToRewardArray");

        _questionDifficultyToLockedStatusArrayProperty = serializedObject.FindProperty("_questionDifficultyToLockedStatusArray");
        _questionDifficultyToCompletedStatusArrayProperty = serializedObject.FindProperty("_questionDifficultyToCompletedStatusArray");

        _questionDifficultyToCostArrayProperty = serializedObject.FindProperty("_questionDifficultyToCostArray");
    }


    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_tag);

        EditorGUILayout.PropertyField(_previewSpriteProperty);
        EditorGUILayout.PropertyField(_captionProperty);

        EditorGUILayout.PropertyField(_questionDifficultyToQuestionListArrayProperty);
        EditorGUILayout.PropertyField(_questionDifficultyToRewardArrayProperty);

        EditorGUILayout.PropertyField(_questionDifficultyToLockedStatusArrayProperty);
        EditorGUILayout.PropertyField(_questionDifficultyToCompletedStatusArrayProperty);
        EditorGUILayout.PropertyField(_questionDifficultyToCostArrayProperty);


        _questionDifficultyToQuestionListArrayProperty.arraySize = Topic.MAX_NUM_OF_DIFFICULTIES;
        _questionDifficultyToRewardArrayProperty.arraySize = Topic.MAX_NUM_OF_DIFFICULTIES;

        _questionDifficultyToLockedStatusArrayProperty.arraySize = Topic.MAX_NUM_OF_DIFFICULTIES;
        _questionDifficultyToCompletedStatusArrayProperty.arraySize = Topic.MAX_NUM_OF_DIFFICULTIES;
        _questionDifficultyToCostArrayProperty.arraySize = Topic.MAX_NUM_OF_DIFFICULTIES;


        QuestionDifficulty nextDif = QuestionDifficulty.Easy;


        for (int i = 0; i < Topic.MAX_NUM_OF_DIFFICULTIES; i++)
        {
            _questionDifficultyToQuestionListArrayProperty.GetArrayElementAtIndex(i).FindPropertyRelative("QuestionDifficulty").enumValueIndex = (int)nextDif;
            _questionDifficultyToRewardArrayProperty.GetArrayElementAtIndex(i).FindPropertyRelative("QuestionDifficulty").enumValueIndex = (int)nextDif;

            _questionDifficultyToLockedStatusArrayProperty.GetArrayElementAtIndex(i).FindPropertyRelative("QuestionDifficulty").enumValueIndex = (int)nextDif;
            _questionDifficultyToCompletedStatusArrayProperty.GetArrayElementAtIndex(i).FindPropertyRelative("QuestionDifficulty").enumValueIndex = (int)nextDif;
            _questionDifficultyToCostArrayProperty.GetArrayElementAtIndex(i).FindPropertyRelative("QuestionDifficulty").enumValueIndex = (int)nextDif;


            nextDif++;
        }


        serializedObject.ApplyModifiedProperties();
    }


}
