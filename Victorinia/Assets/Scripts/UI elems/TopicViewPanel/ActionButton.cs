using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionButton : MonoBehaviour
{

    public enum ButtonState
    {
        play,
        buy,
        completed
    }

    [SerializeField] private ActionCaption _actionCaption;


    [SerializeField] private Image _image;

    [SerializeField] private Sprite _playButtonSprite;
    [SerializeField] private Sprite _buyButtonSprite;

    [SerializeField] private Color _playButtonColor;
    [SerializeField] private Color _buyButtonColor;
    [SerializeField] private Color _completedButtonColor;

    [SerializeField] private Color _lockedBGColor;
    [SerializeField] private Color _unlockedBGColor;
    [SerializeField] private Color _completedBGColor;

    private Button _thisButton;
    private Topic _topic;
    private DifficultyView _difficultyView;

    [SerializeField] private Image _difficultyBackground;

    private ButtonState _buttonState;

    private void Awake()
    {
        _thisButton = GetComponent<Button>();

    }

    private void OnEnable()
    {
        _thisButton.onClick.AddListener(Clicked);
        PlayerEventBus.OnDifficultySelected += UpdateButtonBasedOnDifficulty;
        PlayerEventBus.OnToSelectMenuPressed += () => UpdateButtonBasedOnDifficulty(_difficultyView.CurrentQuestionDifficulty);
        

    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveAllListeners();
        PlayerEventBus.OnDifficultySelected -= UpdateButtonBasedOnDifficulty;
        PlayerEventBus.OnToSelectMenuPressed -= () => UpdateButtonBasedOnDifficulty(_difficultyView.CurrentQuestionDifficulty);
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
        if (_buttonState == ButtonState.play || _buttonState == ButtonState.completed)
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayButton);
            PlayerEventBus.OnStartGame?.Invoke(_topic, _difficultyView.CurrentQuestionDifficulty);
        }
        else if (_buttonState == ButtonState.buy)
        {
            if (PlayerData.TryToChangeMoney(-_topic.GetCostForDifficulty(_difficultyView.CurrentQuestionDifficulty)))
            {
                PlayerData.UnlockDifficulty(_topic.ID, _difficultyView.CurrentQuestionDifficulty);
                UpdateButtonBasedOnDifficulty(_difficultyView.CurrentQuestionDifficulty);
            }
            else // lacking money to buy
            {
                SoundManager.PlaySound(SoundManager.Sound.CantBuy);
                _actionCaption.ChangeToState(ActionCaption.State.noMoney, _topic.GetCostForDifficulty(_difficultyView.CurrentQuestionDifficulty) - PlayerData.Money);
            }
        }

    }

    public void UpdateButtonBasedOnDifficulty(QuestionDifficulty questionDifficulty)
    {
        if (_topic == null) return;

        if (_topic.IsDifficultyLocked(questionDifficulty))
        {
            SetBuyButton(questionDifficulty);
        }
        else
        {
            if (_topic.IsDifficultyCompleted(questionDifficulty))
            {
                SetCompletedButton();
            }
            else
            {
                SetPlayButton(questionDifficulty);
            }

        }

    }


    private void SetBuyButton(QuestionDifficulty questionDifficulty)
    {
        _buttonState = ButtonState.buy;
        _actionCaption.ChangeToState(ActionCaption.State.cost, _topic.GetCostForDifficulty(questionDifficulty));

        _image.sprite = _buyButtonSprite;
        _image.color = _buyButtonColor;

        _difficultyBackground.color = _lockedBGColor;
    }


    private void SetPlayButton(QuestionDifficulty questionDifficulty)
    {
        _actionCaption.ChangeToState(ActionCaption.State.reward, _topic.GetRewardForDifficulty(questionDifficulty));
        _buttonState = ButtonState.play;

        _image.sprite = _playButtonSprite;
        _image.color = _playButtonColor;

        _difficultyBackground.color = _unlockedBGColor;
    }

    private void SetCompletedButton()
    {
        _actionCaption.ChangeToState(ActionCaption.State.completed, 0);
        _buttonState = ButtonState.completed;

        _image.sprite = _playButtonSprite;
        _image.color = _completedButtonColor;

        _difficultyBackground.color = _completedBGColor;


    }

}
