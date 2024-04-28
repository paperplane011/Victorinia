using UnityEngine;

public class DifficultyView : MonoBehaviour
{
    [SerializeField] private QuestionDifficultyToCanvasGroupValue[] _difficultyToCGArray;

    private QuestionDifficulty _currentQuestionDifficulty;

    public QuestionDifficulty CurrentQuestionDifficulty { get { return _currentQuestionDifficulty; } }

    private void Start()
    {
        _currentQuestionDifficulty = QuestionDifficulty.Easy;
        ShowQuestionDifficulty(_currentQuestionDifficulty);
    }

    private void OnEnable()
    {
        PlayerEventBus.OnTopicViewSet += InvokeCurrentlySelectedDifficulty;
    }


    private void OnDisable()
    {
        PlayerEventBus.OnTopicViewSet -= InvokeCurrentlySelectedDifficulty;
    }

    public void GoRight()
    {
        if((int) _currentQuestionDifficulty + 1 >= Topic.MAX_NUM_OF_DIFFICULTIES)
        {
            _currentQuestionDifficulty = 0;
        }
        else
        {
            _currentQuestionDifficulty++;
        }

        ShowQuestionDifficulty(_currentQuestionDifficulty);
    }

    public void GoLeft()
    {
        if ((int)_currentQuestionDifficulty - 1 < 0)
        {
            _currentQuestionDifficulty = (QuestionDifficulty) (Topic.MAX_NUM_OF_DIFFICULTIES-1);
        }
        else
        {
            _currentQuestionDifficulty--;
        }

        ShowQuestionDifficulty(_currentQuestionDifficulty);
    }

    private void InvokeCurrentlySelectedDifficulty()
    {
        PlayerEventBus.OnDifficultySelected?.Invoke(_currentQuestionDifficulty);
    }

    private void ShowQuestionDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach (var elem in _difficultyToCGArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                CanvasUtils.EnableCanvasGroup(elem.CanvasGroup);
                PlayerEventBus.OnDifficultySelected?.Invoke(questionDifficulty);
                Debug.Log("event fired");
            }
            else
            {
                CanvasUtils.DisableCanvasGroup(elem.CanvasGroup);
            }

        }

    }

}
