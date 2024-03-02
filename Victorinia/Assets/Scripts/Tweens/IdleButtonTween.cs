using Tweens;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

[RequireComponent(typeof(EventTrigger))]
public class IdleButtonTween : MonoBehaviour
{
    [SerializeField] private bool _appearTween = true;
    [SerializeField] private bool _loopTween = true;
    [SerializeField] private bool _scaleOnHover = false;

    [Range(0.01f, 10f)]
    [SerializeField] private float _appearDuration;

    [SerializeField] private float _minLoopAngle;
    [SerializeField] private float _maxLoopAngle;
    [SerializeField] private float _loopDuration;

    [Range(1, 5f)]
    [SerializeField] private float _scaleAmount;

    private EventTrigger _eventTrigger;

    private Vector3 _origScale;
    private TweenInstance _zoomInTweenInstance;

    private bool _canBeTweened;

    protected virtual void Awake()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _canBeTweened = true;
        _origScale = transform.localScale;
    }

    protected virtual void Start()
    {
        if (YandexGame.EnvironmentData.isDesktop && _scaleOnHover) AddScaleOnHoverTween();
        if (_loopTween) AddLoopTween();

        // can be optimised: disable tweens in OnDisable(), then enable in OnEnable()
        // but visuals messes up a little bit
    }

    protected virtual void OnEnable()
    {
        transform.localScale = _origScale;
        if (_appearTween) AddAppearTween();
    }


    private void AddAppearTween()
    {
        _origScale = transform.localScale;

        var AppearTween = new Tweens.LocalScaleTween
        {
            from = _origScale * 0.55f,
            to = _origScale,
            duration = _appearDuration,
            easeType = EaseType.BounceOut,
            onStart = (v) => _canBeTweened = false,
            onEnd = (v) => _canBeTweened = true,
        };

        gameObject.AddTween(AppearTween);
    }

    private void AddLoopTween()
    {
        float randomAngle = UnityEngine.Random.Range(_minLoopAngle, _maxLoopAngle);
        bool startLeft = UnityEngine.Random.value > 0.5f ? true : false;

        if (startLeft)
        {
            randomAngle = -randomAngle;
        }

        var LoopTween = new Tweens.RotationTween
        {
            isInfinite = true,
            from = Quaternion.AngleAxis(-randomAngle, Vector3.forward),
            to = Quaternion.AngleAxis(randomAngle, Vector3.forward),
            easeType = EaseType.SineInOut,
            usePingPong = true,
            duration = _loopDuration,

        };

        if (_canBeTweened)
            gameObject.AddTween(LoopTween);
    }

    private void AddScaleOnHoverTween()
    {
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();

        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerExitEntry.eventID = EventTriggerType.PointerExit;


        pointerEnterEntry.callback.AddListener((v) => PointerEnterBehaviour());
        pointerExitEntry.callback.AddListener((v) => PointerExitBehaviour());


        _eventTrigger.triggers.Add(pointerEnterEntry);
        _eventTrigger.triggers.Add(pointerExitEntry);
    }

    public void PointerEnterBehaviour()
    {
        var ZoomIn = new Tweens.LocalScaleTween
        {
            to = _origScale * _scaleAmount,
            duration = 0.3f,
            easeType = EaseType.QuadOut,
        };


        if (_canBeTweened)
            _zoomInTweenInstance = gameObject.AddTween(ZoomIn);

    }

    public void PointerExitBehaviour()
    {
        _zoomInTweenInstance?.Cancel();

        var ZoomOut = new Tweens.LocalScaleTween
        {
            to = _origScale,
            duration = 0.3f,
            easeType = EaseType.QuadIn,

        };

        if (_canBeTweened)
            gameObject.AddTween(ZoomOut);

    }

}
