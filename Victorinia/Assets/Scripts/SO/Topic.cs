using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Topic")]
public class Topic : ScriptableObject
{



    public static readonly int MAX_NUM_OF_DIFFICULTIES = 3;

    public int ID;


    [SerializeField] private Sprite _previewSprite;
    public Sprite PreviewSprite { get { return _previewSprite; } }

    [SerializeField] private string _caption;
    public string Caption { get { return _caption; } }


    [SerializeField] private int _topicCost;
    public int TopicCost { get { return _topicCost; } }

    [SerializeField] private QuestionDifficultyQuestionListValue[] _questionDifficultyToQuestionListArray;
    public QuestionDifficultyQuestionListValue[] QuestionDifficultyToQuestionListArray { get { return _questionDifficultyToQuestionListArray; } }


    [SerializeField] private QuestionDifficultyIntValue[] _questionDifficultyToRewardArray;
    public QuestionDifficultyIntValue[] QuestionDifficultyToRewardArray { get { return _questionDifficultyToRewardArray; } }


    [SerializeField] private QuestionDifficultyBoolValue[] _questionDifficultyToLockedStatusArray;
    public QuestionDifficultyBoolValue[] QuestionDifficultyToLockedStatusArray { get { return _questionDifficultyToLockedStatusArray; } }


    [SerializeField] private QuestionDifficultyIntValue[] _questionDifficultyToCostArray;
    public QuestionDifficultyIntValue[] QuestionDifficultyToCostArray { get { return _questionDifficultyToCostArray; } }


    [SerializeField] private QuestionDifficulty _currentlySelectedDifficulty;
    public QuestionDifficulty CurrentlySelectedDifficulty { get { return _currentlySelectedDifficulty; } }


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
        foreach (var elem in QuestionDifficultyToLockedStatusArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.BoolValue;
            }
        }

        return false;
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

    public void UnlockDifficulty(QuestionDifficulty questionDifficulty) // changes topic
    {
        foreach (var elem in QuestionDifficultyToLockedStatusArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                elem.BoolValue = false;
                SaveTopic();
            }
        }

        

    }

    public void SetRewardToZero(QuestionDifficulty questionDifficulty) // changes topic
    {
        foreach (var elem in QuestionDifficultyToRewardArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                elem.IntValue = 0;
                SaveTopic();
            }
        }

    }


    public int GetRewardForDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach (var elem in QuestionDifficultyToRewardArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.IntValue;
            }
        }

        return 0;
    }


    private void SaveTopic()
    {
        
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

