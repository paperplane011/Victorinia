using Tweens;
using UnityEngine;

public class MoneyViewBalance : MonoBehaviour
{

    public static readonly float COUNTUP_DURATION = 1.4f;


    [SerializeField] private string _prefix;
    [SerializeField] private TMPro.TextMeshProUGUI _prefixTextComp;
    [SerializeField] private TMPro.TextMeshProUGUI _moneyTextComp;


    
    protected virtual void OnEnable()
    {
        PlayerEventBus.OnMoneyChanged += UpdateView;
    }

    protected virtual void OnDisable()
    {
        PlayerEventBus.OnMoneyChanged -= UpdateView;
    }

    private void Start()
    {
        if(_prefixTextComp != null) _prefixTextComp.text = _prefix;
    
        
    }


    protected void UpdateView(int newBalance, int difference)
    {
        //_moneyTextComp.text = newBalance.ToString();
        AddCountupTween(newBalance, difference);
    }


    protected void UpdateView(int newBalance)
    {
        UpdateView(newBalance, 0);
    }

    private void AddCountupTween(int newBalance, int difference)
    {
        var CountupTween = new Tweens.FloatTween
        {
            from = newBalance - difference,
            to = newBalance,
            duration = COUNTUP_DURATION,
            onUpdate = (t, v) => _moneyTextComp.text = ((int) v).ToString(),
            easeType = EaseType.ExpoOut
        };

        gameObject.AddTween(CountupTween);

    }




}
