using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Topic")]
public class Topic : ScriptableObject
{

    public readonly int MAX_NUM_OF_DIFFICULTIES = 3;


    [SerializeField] private Sprite _previewSprite;
    public Sprite PreviewSprite { get { return _previewSprite; } }


    [SerializeField] private int _topicCost;
    public int TopicCost { get { return _topicCost; } }


    [SerializeField] private bool _isTopicLocked;
    public bool IsTopicLocked { get { return _isTopicLocked; } }


    [SerializeField] private QuestionDifficultyQuestionListValue[] _questionListTuple;
    public QuestionDifficultyQuestionListValue[] QuestionListTuple { get { return _questionListTuple; } }


    [SerializeField] private QuestionDifficultyIntValue[] _difficultyRewardTuple;
    public QuestionDifficultyIntValue[] DifficultyRewardTuple { get { return _difficultyRewardTuple; } }


    [SerializeField] private QuestionDifficultyBoolValue[] _difficultyLockedStatusTuple;
    public QuestionDifficultyBoolValue[] DifficultyLockedStatusTuple { get { return _difficultyLockedStatusTuple; } }


    [SerializeField] private QuestionDifficulty _currentlySelectedDifficulty;
    public QuestionDifficulty CurrentlySelectedDifficulty { get { return _currentlySelectedDifficulty; } }


    private void OnValidate()
    {
        _questionListTuple = new QuestionDifficultyQuestionListValue[MAX_NUM_OF_DIFFICULTIES];
        _difficultyRewardTuple = new QuestionDifficultyIntValue[MAX_NUM_OF_DIFFICULTIES];
        _difficultyLockedStatusTuple = new QuestionDifficultyBoolValue[MAX_NUM_OF_DIFFICULTIES];

        QuestionDifficulty questionDifficultyIteration = 0;

        for(int i=0; i < MAX_NUM_OF_DIFFICULTIES; i++)
        {
            _questionListTuple[i] = new();
            _difficultyRewardTuple[i] = new();
            _difficultyLockedStatusTuple[i] = new();


            _questionListTuple[i].QuestionDifficulty = questionDifficultyIteration;
            _difficultyRewardTuple[i].QuestionDifficulty = questionDifficultyIteration;
            _difficultyLockedStatusTuple[i].QuestionDifficulty = questionDifficultyIteration;

            questionDifficultyIteration++;
        }

    }


}






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

