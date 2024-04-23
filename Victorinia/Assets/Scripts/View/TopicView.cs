using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TopicView : MonoBehaviour
{
    [Header("Component hooks")]
    [SerializeField] private Image _previewImage;

    [SerializeField] private TextMeshProUGUI _captionTextComp;
    [SerializeField] private TextMeshProUGUI _topicCostTextComp;

    [SerializeField] private QuestionDifficultyToCostRewardValue[] _questionDifficultyToCostRewardTextCompArray;
    [SerializeField] private QuestionDifficultyToCanvasGroupValue[] _questionDifficultyToDifficultyLockCanvasGroupArray;

    [SerializeField] private CanvasGroup _topicLockCanvasGroup;


    public void SetTopicVisuals(Topic topic)
    {
        _previewImage.sprite = topic.PreviewSprite;
        _captionTextComp.text = topic.Caption;
        _topicCostTextComp.text = topic.TopicCost.ToString();

        AssignDifficultiesCostAndReward(topic);

        LockTopicIfNeeded(topic);
        LockLockedDifficulties(topic);
    }



    private void AssignDifficultiesCostAndReward(Topic topic)
    {
        for(int i=0; i<Topic.MAX_NUM_OF_DIFFICULTIES; i++)
        {
            _questionDifficultyToCostRewardTextCompArray[0].CostTextComp.text = topic.QuestionDifficultyToCostArray[0].IntValue.ToString();
            _questionDifficultyToCostRewardTextCompArray[0].RewardTextComp.text = topic.QuestionDifficultyToRewardArray[0].IntValue.ToString();
        }
    }

    private void LockTopicIfNeeded(Topic topic)
    {
        if (topic.IsTopicLocked)
        {
            CanvasUtils.EnableCanvasGroup(_topicLockCanvasGroup);
        }
        else
        {
            CanvasUtils.DisableCanvasGroup(_topicLockCanvasGroup);
        }
    }

    private void LockLockedDifficulties(Topic topic)
    {
        for(int i=0; i<Topic.MAX_NUM_OF_DIFFICULTIES; i++)
        {
            if (topic.QuestionDifficultyToLockedStatusArray[i].BoolValue)
            {
                CanvasUtils.EnableCanvasGroup(_questionDifficultyToDifficultyLockCanvasGroupArray[i].CanvasGroup);
            }
            else
            {
                CanvasUtils.DisableCanvasGroup(_questionDifficultyToDifficultyLockCanvasGroupArray[i].CanvasGroup);
            }
        }
    }
    

    private void OnValidate()
    {
        _questionDifficultyToCostRewardTextCompArray = new QuestionDifficultyToCostRewardValue[Topic.MAX_NUM_OF_DIFFICULTIES];
        _questionDifficultyToDifficultyLockCanvasGroupArray = new QuestionDifficultyToCanvasGroupValue[Topic.MAX_NUM_OF_DIFFICULTIES];

        QuestionDifficulty questionDifficultyIteration = 0;

        for (int i = 0; i < Topic.MAX_NUM_OF_DIFFICULTIES; i++)
        {
            _questionDifficultyToCostRewardTextCompArray[i] = new();
            _questionDifficultyToDifficultyLockCanvasGroupArray[i] = new();

            _questionDifficultyToCostRewardTextCompArray[i].QuestionDifficulty = questionDifficultyIteration;
            _questionDifficultyToDifficultyLockCanvasGroupArray[i].QuestionDifficulty = questionDifficultyIteration;

            questionDifficultyIteration++;
        }
    }

    [Serializable]
    private class QuestionDifficultyToCostRewardValue
    {
        public QuestionDifficulty QuestionDifficulty;
        public TMPro.TextMeshProUGUI CostTextComp;
        public TMPro.TextMeshProUGUI RewardTextComp;
    }

    [Serializable]
    private class QuestionDifficultyToCanvasGroupValue
    {
        public QuestionDifficulty QuestionDifficulty;
        public CanvasGroup CanvasGroup;
    }

}


