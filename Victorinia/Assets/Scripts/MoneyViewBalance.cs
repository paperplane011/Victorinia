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
        _prefixTextComp.text = _prefix;
    }


    private void UpdateView(int value)
    {
        _moneyTextComp.text = value.ToString();
    }



}
