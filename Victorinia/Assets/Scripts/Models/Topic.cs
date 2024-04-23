using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Topic")]
public class Topic : ScriptableObject
{

    public static readonly int MAX_NUM_OF_DIFFICULTIES = 3;


    [SerializeField] private Sprite _previewSprite;
    public Sprite PreviewSprite { get { return _previewSprite; } }

    [SerializeField] private string _caption;
    public string Caption { get { return _caption; } }


    [SerializeField] private int _topicCost;
    public int TopicCost { get { return _topicCost; } }


    [SerializeField] private bool _isTopicLocked;
    public bool IsTopicLocked { get { return _isTopicLocked; } }


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


    private void OnValidate()
    {
        _questionDifficultyToQuestionListArray = new QuestionDifficultyQuestionListValue[MAX_NUM_OF_DIFFICULTIES];
        _questionDifficultyToCostArray = new QuestionDifficultyIntValue[MAX_NUM_OF_DIFFICULTIES];
        _questionDifficultyToRewardArray = new QuestionDifficultyIntValue[MAX_NUM_OF_DIFFICULTIES];
        _questionDifficultyToLockedStatusArray = new QuestionDifficultyBoolValue[MAX_NUM_OF_DIFFICULTIES];

        QuestionDifficulty questionDifficultyIteration = 0;

        for(int i=0; i < MAX_NUM_OF_DIFFICULTIES; i++)
        {
            _questionDifficultyToQuestionListArray[i] = new();
            _questionDifficultyToRewardArray[i] = new();
            _questionDifficultyToCostArray[i] = new();
            _questionDifficultyToLockedStatusArray[i] = new();


            _questionDifficultyToQuestionListArray[i].QuestionDifficulty = questionDifficultyIteration;
            _questionDifficultyToRewardArray[i].QuestionDifficulty = questionDifficultyIteration;
            _questionDifficultyToCostArray[i].QuestionDifficulty = questionDifficultyIteration;
            _questionDifficultyToLockedStatusArray[i].QuestionDifficulty = questionDifficultyIteration;

            questionDifficultyIteration++;
        }

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

