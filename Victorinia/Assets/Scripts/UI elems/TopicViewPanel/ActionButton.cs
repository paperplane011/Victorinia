using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButton : MonoBehaviour
{

    public enum ButtonState
    {
        play,
        buy
    }

    [SerializeField] private ActionCaption _actionCaption;
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
        PlayerEventBus.OnDifficultySelected += SetButtonStateBasedOnDifficulty;
        
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveAllListeners();
        PlayerEventBus.OnDifficultySelected -= SetButtonStateBasedOnDifficulty;
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
            PlayerEventBus.OnStartGame?.Invoke(_topic, _difficultyView.CurrentQuestionDifficulty);
        }

        if(_buttonState == ButtonState.buy)
        {
            if (PlayerData.TryToChangeMoney(-_topic.GetCostForDifficulty(_difficultyView.CurrentQuestionDifficulty)))
            {
                _topic.UnlockDifficulty(_difficultyView.CurrentQuestionDifficulty);
                SetButtonStateBasedOnDifficulty(_difficultyView.CurrentQuestionDifficulty);
            }
            else // lacking money to buy
            {
                _actionCaption.ChangeToState(ActionCaption.State.noMoney, _topic.GetCostForDifficulty(_difficultyView.CurrentQuestionDifficulty) - PlayerData.Money);
            }            
        }

    }

    public void SetButtonStateBasedOnDifficulty(QuestionDifficulty questionDifficulty)
    {
        if (_topic.IsDifficultyLocked(questionDifficulty))
        {
            _buttonState = ButtonState.buy;
            _actionCaption.ChangeToState(ActionCaption.State.cost, _topic.GetCostForDifficulty(questionDifficulty));
            _thisButton.image.color = Color.red;
            
            SetBuyButton();
        }
        else
        {
            _actionCaption.ChangeToState(ActionCaption.State.reward, _topic.GetRewardForDifficulty(questionDifficulty));
            _thisButton.image.color = Color.green;
            _buttonState = ButtonState.play;
            SetPlayButton();
            
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
