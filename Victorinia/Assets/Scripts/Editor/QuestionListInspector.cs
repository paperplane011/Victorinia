
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(QuestionList))]
public class QuestionListInspector : Editor
{

    SerializedProperty _thisQuestionListDifficulty;
    SerializedProperty _tagToFill;
    SerializedProperty _thisQuestionList;
    
    

   

    private void OnEnable()
    {
        _thisQuestionListDifficulty = serializedObject.FindProperty("_thisQuestionListDifficulty");
        _tagToFill = serializedObject.FindProperty("TagToFill");

        _thisQuestionList = serializedObject.FindProperty("_thisQuestionList");
    }

 

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_thisQuestionListDifficulty);
        EditorGUILayout.PropertyField(_tagToFill);

        

        EditorGUILayout.PropertyField(_thisQuestionList);
      
        


        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }



    


}
