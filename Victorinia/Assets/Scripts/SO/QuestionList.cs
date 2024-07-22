using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionList")]
public class QuestionList : ScriptableObject
{
    [SerializeField] private QuestionDifficulty _thisQuestionListDifficulty;
    [SerializeField] private List<Question> _thisQuestionList;


    public QuestionDifficulty ThisQuestionListDifficulty { get { return _thisQuestionListDifficulty; } }
    public string TagToFill;
    public List<Question> ThisQuestionList { get { return _thisQuestionList; } }




#if UNITY_EDITOR

    // ƒобавл€ет вопросы с сложностью ThisQuestionListDifficulty и тегом TagToFill в этот QuestionList

    private const string QUESTION_SEARCH_FILTER = "t:Question";
    

    [ContextMenu("Fill List")]
    public void FillList()
    {
        ThisQuestionList.Clear();

        string[] allQuestionGUIDs = AssetDatabase.FindAssets(QUESTION_SEARCH_FILTER);

        foreach(var questionGUID in allQuestionGUIDs)
        {
            string questionPath = AssetDatabase.GUIDToAssetPath(questionGUID);

            Question question = AssetDatabase.LoadAssetAtPath<Question>(questionPath);

            if(question.Tag == TagToFill && question.QuestionDifficulty == _thisQuestionListDifficulty)
            {
                _thisQuestionList.Add(question);
            }
        }        
    }

    

#endif


    
}
