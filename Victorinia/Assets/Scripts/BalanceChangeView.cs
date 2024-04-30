using Tweens;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BalanceChangeView : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI _moneyChangeTextComp;

    private CanvasGroup _canvasGroup;
    private float _origMoneyChangeYPos;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasGroup.alpha = 0;
        _origMoneyChangeYPos = transform.localPosition.y;

    }

    private void OnEnable()
    {
        PlayerEventBus.OnMoneyChanged += UpdateView;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnMoneyChanged -= UpdateView;
    }

    private void UpdateView(int newBalance, int difference)
    {
        if (difference > 0)
        {
            _moneyChangeTextComp.color = Color.green;
        }
        else
        {
            _moneyChangeTextComp.color = Color.red;
        }

        _moneyChangeTextComp.text = ((difference > 0) ? "+" : "") + difference.ToString();

        AddAppearTween();


    }


    private void AddAppearTween()
    {

        var AppearTween = new Tweens.LocalPositionYTween
        {
            from = _origMoneyChangeYPos,
            to = _origMoneyChangeYPos + 40,
            duration = 1f,
            easeType = Tweens.EaseType.SineInOut,
            onStart = (i) => _canvasGroup.alpha = 1,
            onEnd = (i) => _canvasGroup.alpha = 0,
            onUpdate = (i, v) => Debug.Log(v.ToString())
        };

        gameObject.AddTween(AppearTween);
    }

}
