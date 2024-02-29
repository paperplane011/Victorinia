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

    private Vector3 _origScale;
    private TweenInstance _zoomInTweenInstance;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);

        _button.interactable = true;

        PlayerEventsInvoker.OnAnswerPressed += AllAnswerBehaviour;

        Quaternion currentRotation = gameObject.transform.rotation;

        var AppearTween = new Tweens.LocalScaleTween
        {
            from = _origScale * 0.55f,
            to = _origScale,
            duration = 0.6f,
            easeType = EaseType.BounceOut
        };


        float randomAngle = UnityEngine.Random.Range(1f, 3f);
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
        PlayerEventsInvoker.OnAnswerPressed -= AllAnswerBehaviour;
    }


    private void Start()
    {
        _origScale = transform.localScale;
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
        };

        gameObject.AddTween(corAnswerGoUp);
    }



    public void PointerEnterBehaviour()
    {
        var ZoomIn = new Tweens.LocalScaleTween
        {
            to = _origScale * 1.2f,
            duration = 0.3f,
            easeType = EaseType.QuadOut,
        };


        _zoomInTweenInstance = gameObject.AddTween(ZoomIn);

    }

    public void PointerExitBehaviour()
    {
        _zoomInTweenInstance.Cancel();

        var ZoomOut = new Tweens.LocalScaleTween
        {
            to = _origScale,
            duration = 0.3f,
            easeType = EaseType.QuadIn,

        };

        gameObject.AddTween(ZoomOut);
        
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
            PlayerEventsInvoker.OnAnswerPressed?.Invoke(true);
        }
        else
        {
            PlayerEventsInvoker.OnAnswerPressed?.Invoke(false);
        }
    }






}
