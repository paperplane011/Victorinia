using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ActionCaption), typeof(Button))]
public class ActionButton : MonoBehaviour
{

    private Button _thisButton;
    private Topic _topic;
    private DifficultyView _difficultyView;

    private bool _isButtonLocked;

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
        if (!_isButtonLocked)
        {
            PlayerEventBus.OnStartGame?.Invoke(_difficultyView.CurrentQuestionDifficulty);
        }
    }

    public void SetDifficulty(QuestionDifficulty questionDifficulty)
    {
        if (IsDifficultyLocked(questionDifficulty))
        {
           _thisButton.image.color = Color.red;
            _isButtonLocked = true;
        }
        else
        {
            _thisButton.image.color = Color.green;
            _isButtonLocked = false;
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

}
