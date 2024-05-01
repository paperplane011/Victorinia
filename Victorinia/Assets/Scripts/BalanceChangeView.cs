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

        AddCountdownTween(difference);


    }


    private void AddCountdownTween(int difference)
    {

        var CountdownTween = new Tweens.FloatTween
        {
            from = difference,
            to = 0,
            duration = MoneyViewBalance.COUNTUP_DURATION,
            onStart = (i) => _canvasGroup.alpha = 1,
            onEnd = (i) => _canvasGroup.alpha = 0,
            onUpdate = (t, v) => _moneyChangeTextComp.text = ((int)v).ToString(),
            easeType = EaseType.SineInOut
        };

        gameObject.AddTween(CountdownTween);
    }

}
