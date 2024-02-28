using System.Collections;
using Tweens;
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
        PlayerEventsInvoker.OnAnswerPressed += WrongAnswerBehaviour;

        Vector3 currentScale = gameObject.transform.localScale;
        Quaternion currentRotation = gameObject.transform.rotation;

        var AppearTween = new Tweens.LocalScaleTween
        {
            from = currentScale * 0.55f,
            to = currentScale,
            duration = 0.6f,
            easeType = EaseType.BounceOut
        };


        float randomAngle = UnityEngine.Random.Range(2f, 4f);
        bool startLeft = UnityEngine.Random.value > 0.5f ? true : false;

        if (startLeft)
        {
            randomAngle *= -1;
        }

        var LoopTween = new Tweens.RotationTween
        {
            isInfinite = true,
            from = Quaternion.AngleAxis(-randomAngle, Vector3.forward),
            to = Quaternion.AngleAxis(randomAngle, Vector3.forward),
            easeType = EaseType.SineInOut,
            usePingPong = true,
            duration = 1f,

        };


        gameObject.AddTween(AppearTween);
        gameObject.AddTween(LoopTween);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
        PlayerEventsInvoker.OnAnswerPressed -= WrongAnswerBehaviour;
    }




    private void WrongAnswerBehaviour(bool isClickedAnswerCorrect)
    {
        if (isClickedAnswerCorrect == true) return;

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
            onEnd = (v) => gameObject.AddTween(wrongAnswerGoDown)
        };

        gameObject.AddTween(wrongAnswerGoUp);
    }

    private void CorrectAnswerBehaviour()
    {
        PositionYTween corAnswerGoUp = new PositionYTween
        {
            to = gameObject.transform.position.y + UnityEngine.Random.Range(120f, 150f),
            easeType = EaseType.BackOut,
            duration = UnityEngine.Random.Range(0.3f, 0.4f),
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
            CorrectAnswerBehaviour();
            PlayerEventsInvoker.OnAnswerPressed?.Invoke(true);
        }
        else
        {
            PlayerEventsInvoker.OnAnswerPressed?.Invoke(false);
        }
    }

   




}
