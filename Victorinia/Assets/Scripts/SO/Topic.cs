using System;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Topic")]
public class Topic : ScriptableObject
{



    public static readonly int MAX_NUM_OF_DIFFICULTIES = 3;

    public int ID;
    
    [SerializeField] private string _tag;


    [SerializeField] private Sprite _previewSprite;
    public Sprite PreviewSprite { get { return _previewSprite; } }

    [SerializeField] private string _caption;
    public string Caption { get { return _caption; } }



    [SerializeField] private QuestionDifficultyQuestionListValue[] _questionDifficultyToQuestionListArray;
    public QuestionDifficultyQuestionListValue[] QuestionDifficultyToQuestionListArray { get { return _questionDifficultyToQuestionListArray; } }


    [SerializeField] private QuestionDifficultyIntValue[] _questionDifficultyToRewardArray;
    public QuestionDifficultyIntValue[] QuestionDifficultyToRewardArray { get { return _questionDifficultyToRewardArray; } }


    [SerializeField] private QuestionDifficultyBoolValue[] _questionDifficultyToLockedStatusArray;
    public QuestionDifficultyBoolValue[] QuestionDifficultyToLockedStatusArray { get { return _questionDifficultyToLockedStatusArray; } }


    [SerializeField] private QuestionDifficultyIntValue[] _questionDifficultyToCostArray;
    public QuestionDifficultyIntValue[] QuestionDifficultyToCostArray { get { return _questionDifficultyToCostArray; } }

#if UNITY_EDITOR

    private const string QUESTION_LIST_SEARCH_FILTER = "t:QuestionList";

    [ContextMenu("Fill Topic")]
    public void FillTopic()
    {

        string[] allQuestionListGUIDs = AssetDatabase.FindAssets(QUESTION_LIST_SEARCH_FILTER);

        foreach (var questionListGUID in allQuestionListGUIDs)
        {
            string questionListPath = AssetDatabase.GUIDToAssetPath(questionListGUID);

            QuestionList questionList = AssetDatabase.LoadAssetAtPath<QuestionList>(questionListPath);

            questionList.FillList();
            
            if (questionList.TagToFill == _tag)
            {
               
                switch (questionList.ThisQuestionListDifficulty)
                {
                    case QuestionDifficulty.Easy:
                        _questionDifficultyToQuestionListArray[0] = new QuestionDifficultyQuestionListValue();
                        _questionDifficultyToQuestionListArray[0].QuestionDifficulty = QuestionDifficulty.Easy;
                        _questionDifficultyToQuestionListArray[0].QuestionList = questionList;
                        break;
                    case QuestionDifficulty.Normal:
                        _questionDifficultyToQuestionListArray[1] = new QuestionDifficultyQuestionListValue();
                        _questionDifficultyToQuestionListArray[1].QuestionDifficulty = QuestionDifficulty.Normal;
                        _questionDifficultyToQuestionListArray[1].QuestionList = questionList;
                        break;
                    case QuestionDifficulty.Hard:
                        _questionDifficultyToQuestionListArray[2] = new QuestionDifficultyQuestionListValue();
                        _questionDifficultyToQuestionListArray[2].QuestionDifficulty = QuestionDifficulty.Hard;
                        _questionDifficultyToQuestionListArray[2].QuestionList = questionList;
                        break;
                };


            }
        }

    }


#endif


    //private void OnValidate()
    //{


    //    _questionDifficultyToQuestionListArray = new QuestionDifficultyQuestionListValue[MAX_NUM_OF_DIFFICULTIES];
    //    _questionDifficultyToCostArray = new QuestionDifficultyIntValue[MAX_NUM_OF_DIFFICULTIES];
    //    _questionDifficultyToRewardArray = new QuestionDifficultyIntValue[MAX_NUM_OF_DIFFICULTIES];
    //    _questionDifficultyToLockedStatusArray = new QuestionDifficultyBoolValue[MAX_NUM_OF_DIFFICULTIES];

    //    QuestionDifficulty questionDifficultyIteration = 0;

    //    for(int i=0; i < MAX_NUM_OF_DIFFICULTIES; i++)
    //    {
    //        _questionDifficultyToQuestionListArray[i] = new();
    //        _questionDifficultyToRewardArray[i] = new();
    //        _questionDifficultyToCostArray[i] = new();
    //        _questionDifficultyToLockedStatusArray[i] = new();


    //        _questionDifficultyToQuestionListArray[i].QuestionDifficulty = questionDifficultyIteration;
    //        _questionDifficultyToRewardArray[i].QuestionDifficulty = questionDifficultyIteration;
    //        _questionDifficultyToCostArray[i].QuestionDifficulty = questionDifficultyIteration;
    //        _questionDifficultyToLockedStatusArray[i].QuestionDifficulty = questionDifficultyIteration;

    //        questionDifficultyIteration++;
    //    }




    //}


    public bool IsDifficultyLocked(QuestionDifficulty questionDifficulty)
    {
        return PlayerData.IsTopicDifficultyLocked(ID, questionDifficulty);
    }

    public QuestionList GetQuestionListForDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach (var elem in QuestionDifficultyToQuestionListArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.QuestionList;
            }
        }

        throw new ArgumentException(questionDifficulty.ToString());

    }

    public int GetCostForDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach (var elem in QuestionDifficultyToCostArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.IntValue;
            }
        }

        throw new ArgumentException(questionDifficulty.ToString());
    }



    public int GetRewardForDifficulty(QuestionDifficulty questionDifficulty)
    {
        return PlayerData.GetTopicRewardForDifficulty(ID, questionDifficulty);
    }


    

    public TopicSave ToTopicSave()
    {
        return new TopicSave(this);
    }

}




// maybe there is no need for constructors below ?

[Serializable]
public class QuestionDifficultyQuestionListValue
{
    public QuestionDifficulty QuestionDifficulty;
    public QuestionList QuestionList;

    public QuestionDifficultyQuestionListValue()
    {
        QuestionDifficulty = QuestionDifficulty.Easy;
        //QuestionList = null;
    }
}


[Serializable]
public class QuestionDifficultyIntValue 
{
    public QuestionDifficulty QuestionDifficulty;
    public int IntValue;

    public QuestionDifficultyIntValue()
    {
        QuestionDifficulty = QuestionDifficulty.Easy;
        IntValue = 0;

    }
}

[Serializable]
public class QuestionDifficultyBoolValue 
{
    public QuestionDifficulty QuestionDifficulty;
    public bool BoolValue;

    public QuestionDifficultyBoolValue()
    {
        QuestionDifficulty = QuestionDifficulty.Easy;

    }
}

