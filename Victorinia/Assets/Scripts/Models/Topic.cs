using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Topic")]
public class Topic : ScriptableObject
{

    [field: SerializeField] public Sprite PreviewSprite { get; private set; }
    [field: SerializeField] public bool IsTopicLocked { get; private set; }
    [field: SerializeField] public int _topicCost { get; private set; }
    [field: SerializeField] public Tuple<QuestionDifficulty, QuestionList> _thisTopicQuestionListTuple { get; private set; }
    [field: SerializeField] public Tuple<QuestionDifficulty, int> _rewardTuple { get; private set; }
    [field: SerializeField] public QuestionDifficulty _currentlySelectedDifficulty { get; private set; }

    [field: SerializeField] public Tuple<QuestionDifficulty, QuestionList> _difficultyRewardTuple { get; private set; }

}
