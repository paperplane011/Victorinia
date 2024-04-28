using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TopicView : MonoBehaviour
{
    [Header("Component hooks")]
    [SerializeField] private Image _previewImage;

    [SerializeField] private TextMeshProUGUI _captionTextComp;

    [SerializeField] private ActionButton _actionButton;
    [SerializeField] private DifficultyView _difficultyView;

    [SerializeField] private CanvasGroup _topicLockCanvasGroup;


    private void Start()
    {
        _actionButton.SetDifficultyView(_difficultyView);
    }

    public void SetTopic(Topic topic)
    {
        _previewImage.sprite = topic.PreviewSprite;
        _captionTextComp.text = topic.Caption;
    

        LockTopicIfNeeded(topic);
        _actionButton.SetTopic(topic);
        
        
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



    //private void OnValidate()
    //{
    //    _questionDifficultyToCostRewardTextCompArray = new QuestionDifficultyToCostRewardValue[Topic.MAX_NUM_OF_DIFFICULTIES];
    //    _questionDifficultyToDifficultyLockCanvasGroupArray = new QuestionDifficultyToCanvasGroupValue[Topic.MAX_NUM_OF_DIFFICULTIES];

    //    QuestionDifficulty questionDifficultyIteration = 0;

    //    for (int i = 0; i < Topic.MAX_NUM_OF_DIFFICULTIES; i++)
    //    {
    //        _questionDifficultyToCostRewardTextCompArray[i] = new();
    //        _questionDifficultyToDifficultyLockCanvasGroupArray[i] = new();

    //        _questionDifficultyToCostRewardTextCompArray[i].QuestionDifficulty = questionDifficultyIteration;
    //        _questionDifficultyToDifficultyLockCanvasGroupArray[i].QuestionDifficulty = questionDifficultyIteration;

    //        questionDifficultyIteration++;
    //    }
    //}

    [Serializable]
    private class QuestionDifficultyToCostRewardValue
    {
        public QuestionDifficulty QuestionDifficulty;
        public TMPro.TextMeshProUGUI CostTextComp;
        public TMPro.TextMeshProUGUI RewardTextComp;
    }

   

}


[Serializable]
public class QuestionDifficultyToCanvasGroupValue
{
    public QuestionDifficulty QuestionDifficulty;
    public CanvasGroup CanvasGroup;
}

