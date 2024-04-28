using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ActionCaption), typeof(Button))]
public class ActionButton : MonoBehaviour
{

    public enum ButtonState
    {
        play,
        buy
    }

    [SerializeField] private CanvasGroup _buyButtonCG;
    [SerializeField] private CanvasGroup _playButtonCG;

    private Button _thisButton;
    private Topic _topic;
    private DifficultyView _difficultyView;

    private ButtonState _buttonState;

    private void Awake()
    {
        _thisButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _thisButton.onClick.AddListener(Clicked);
        PlayerEventBus.OnDifficultySelected += SetDifficulty;
        
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveAllListeners();
        PlayerEventBus.OnDifficultySelected -= SetDifficulty;
    }

    public void SetTopic(Topic topic)
    {
        _topic = topic;
    }

    public void SetDifficultyView(DifficultyView difficultyView)
    {
        _difficultyView = difficultyView;
    }

    private void Clicked()
    {
        if (_buttonState == ButtonState.play)
        {
            PlayerEventBus.OnStartGame?.Invoke(GetQuestionListForDifficulty(_difficultyView.CurrentQuestionDifficulty));
        }

        if(_buttonState == ButtonState.buy)
        {
            // buy
            PlayerData.ChangeMoney(-GetCostForDifficulty(_difficultyView.CurrentQuestionDifficulty));

            UnlockDifficulty(_difficultyView.CurrentQuestionDifficulty);
            SetDifficulty(_difficultyView.CurrentQuestionDifficulty);
            
        }

    }

    public void SetDifficulty(QuestionDifficulty questionDifficulty)
    {
        if (IsDifficultyLocked(questionDifficulty))
        {
            _buttonState = ButtonState.buy;
            _thisButton.image.color = Color.red;
            
            SetBuyButton();
        }
        else
        {
            _thisButton.image.color = Color.green;
            _buttonState = ButtonState.play;
            SetPlayButton();
            
        }


    }

    private bool IsDifficultyLocked(QuestionDifficulty questionDifficulty) 
    {
        foreach(var elem in _topic.QuestionDifficultyToLockedStatusArray)
        {
            if(elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.BoolValue;
            }
        }

        return false;
    }

    private QuestionList GetQuestionListForDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach (var elem in _topic.QuestionDifficultyToQuestionListArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.QuestionList;
            }
        }

        throw new ArgumentException(questionDifficulty.ToString());
       
    }

    private int GetCostForDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach (var elem in _topic.QuestionDifficultyToCostArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.IntValue;
            }
        }

        throw new ArgumentException(questionDifficulty.ToString());
    }

    private void UnlockDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach (var elem in _topic.QuestionDifficultyToLockedStatusArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                elem.BoolValue = false;
            }
        }
    }


    private void SetBuyButton()
    {
        //CanvasUtils.EnableCanvasGroup(_buyButtonCG);
        // action caption
    }


    private void SetPlayButton()
    {
        //CanvasUtils.EnableCanvasGroup(_playButtonCG);

    }

}
