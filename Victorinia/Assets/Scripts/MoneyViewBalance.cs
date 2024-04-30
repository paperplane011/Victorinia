using Tweens;
using UnityEngine;

public class MoneyViewBalance : MonoBehaviour
{


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
        

        _moneyTextComp.text = newBalance.ToString();
    }


    protected void UpdateView(int newBalance)
    {
        UpdateView(newBalance, 0);

    }



}
