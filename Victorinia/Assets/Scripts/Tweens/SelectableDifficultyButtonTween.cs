using Tweens;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(DifficultySetButton))]
public class SelectableDifficultyButtonTween : IdleTween
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _selectDuration;

    private Button _button;
    private DifficultySetButton _difficultySetButton;
    

    private float _origX;
    private bool _isOrigXSetted;

    private bool _isButtonSelected;

    private TweenInstance _selectInstance;
    private TweenInstance _deselectInstance;

    protected override void Awake()
    {
        base.Awake();
        _button = GetComponentInChildren<Button>();
        _difficultySetButton = GetComponent<DifficultySetButton>();
        
        _isButtonSelected = false;
    }


    protected override void OnEnable()
    {
        base.OnEnable();

        _button.onClick.AddListener(ButtonClickedBehaviour);
        PlayerEventsInvoker.OnPlayerDifficultySelected += OtherDifficultySelectedBehaviour;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
        PlayerEventsInvoker.OnPlayerDifficultySelected -= OtherDifficultySelectedBehaviour;
    }

    protected override void Start()
    {
        base.Start();
        _isOrigXSetted = false;
    }


    private void ButtonClickedBehaviour()
    {
        _isButtonSelected = !_isButtonSelected;

        if (_isButtonSelected)
        {
            if (!_isOrigXSetted)
            {
                _origX = transform.position.x;
                _isOrigXSetted = true;
            }

            AddSelectTween();
        }
        else
        {
            AddDeselectTween();
        }

    }

    private void AddSelectTween()
    {
        _deselectInstance?.Cancel();

        Tweens.PositionXTween Select = new Tweens.PositionXTween()
        {
            to = _origX + _offsetX,
            duration = _selectDuration,
            easeType = Tweens.EaseType.SineOut
        };

        
        _selectInstance = gameObject.AddTween(Select);
    }

    private void AddDeselectTween()
    {
        _selectInstance?.Cancel();

        Tweens.PositionXTween Deselect = new Tweens.PositionXTween()
        {
            to = _origX,
            duration = _selectDuration,
            easeType = Tweens.EaseType.SineIn
        };

        _deselectInstance = gameObject.AddTween(Deselect);
    }


    private void OtherDifficultySelectedBehaviour(QuestionDifficulty questionDifficulty)
    {
        if (!_isButtonSelected) return;

        if (_difficultySetButton.SelectableDifficulty != questionDifficulty)
        {
            AddDeselectTween();
            _isButtonSelected = false;
        }


    }
}
