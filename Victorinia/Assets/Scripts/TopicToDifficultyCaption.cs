using Unity.VisualScripting;
using UnityEngine;

public class TopicToDifficultyCaption : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI _textComp;
    [SerializeField] private string _prefix;
    [SerializeField] private string _uncompletedPostfix;
    [SerializeField] private string _completedPostfix;


    private void OnEnable()
    {
        PlayerEventBus.OnStartGame += UpdateText;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnStartGame -= UpdateText;
    }


    private void UpdateText(Topic topic, QuestionDifficulty questionDifficulty)
    {
        _textComp.text = _prefix + $"{topic.Caption} ({GetStringForQuestionDifficulty(questionDifficulty)})" + ((topic.IsDifficultyCompleted(questionDifficulty) || topic.GetRewardForDifficulty(questionDifficulty) == 0) ? _completedPostfix : _uncompletedPostfix);
    }


    private string GetStringForQuestionDifficulty(QuestionDifficulty questionDifficulty)
    {
        switch (questionDifficulty)
        {
            case QuestionDifficulty.Easy:
                return "Легко";
            case QuestionDifficulty.Normal:
                return "Нормально";
            case QuestionDifficulty.Hard:
                return "Сложно";
        }

        return "";
    }


}
