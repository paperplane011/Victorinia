using Tweens;
using Tweens.Core;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{

    public bool IsCorrect { get; private set; }

    [Header("Component hooks")]
    [SerializeField] private TMPro.TextMeshProUGUI _textComponent;
    private Button _button;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);

        _button.interactable = true;

        PlayerEventBus.OnAnswerPressed += AllAnswerBehaviour;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
        PlayerEventBus.OnAnswerPressed -= AllAnswerBehaviour;
    }


   

    // called for all buttons when answer clicked
    private void AllAnswerBehaviour(bool isClickedAnswerCorrect)
    {
        _button.interactable = false;

        if (isClickedAnswerCorrect == true)
        {

        }
        else
        {
            AllWrongAnswerBehaviour();
        }
    }

    // called for all buttons when wrong answer clicked
    private void AllWrongAnswerBehaviour() 
    {
        _button.interactable = false;

        if (IsCorrect) return; // correct answer stays on screen

        PositionYTween wrongAnswerGoDown = new PositionYTween
        {
            to = gameObject.transform.position.y - 1200,
            easeType = EaseType.CubicIn,
            duration = UnityEngine.Random.Range(0.6f, 0.8f),
        };

        PositionYTween wrongAnswerGoUp = new PositionYTween
        {
            to = gameObject.transform.position.y + UnityEngine.Random.Range(120f, 250f),
            easeType = EaseType.CubicOut,
            duration = UnityEngine.Random.Range(0.1f, 0.25f),
            onStart = (v) => BlockRaycastScreen.SetBlockRaycastStatus?.Invoke(true),
            onEnd = (v) => gameObject.AddTween(wrongAnswerGoDown)
        };

        gameObject.AddTween(wrongAnswerGoUp);
    }


    
    private void OneCorrectAnswerBehaviour()
    {
        PositionYTween corAnswerGoUp = new PositionYTween
        {
            to = gameObject.transform.position.y + UnityEngine.Random.Range(120f, 150f),
            easeType = EaseType.BackOut,
            duration = UnityEngine.Random.Range(0.3f, 0.4f),
            onStart = (v) => BlockRaycastScreen.SetBlockRaycastStatus?.Invoke(true)
           

        };

        gameObject.AddTween(corAnswerGoUp);
    }



    public void AssignAnswer(Answer answer)
    {
        _textComponent.text = answer.AnswerText;
        IsCorrect = answer.IsCorrect;
    }


    public void Clicked()
    {
        if (IsCorrect)
        {
            OneCorrectAnswerBehaviour();
            SoundManager.PlaySound(SoundManager.Sound.AnswerCorrect);
            PlayerEventBus.OnAnswerPressed?.Invoke(true);
        }
        else
        {

            SoundManager.PlaySound(SoundManager.Sound.AnswerIncorrect);
            PlayerEventBus.OnAnswerPressed?.Invoke(false);
        }
    }






}
