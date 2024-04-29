using UnityEngine;

public class MoneyView : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI _textComp;

    [SerializeField] private string _prefix;
    private string _value;

    

    private void OnEnable()
    {
        PlayerEventBus.OnMoneyChanged += UpdateView;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnMoneyChanged -= UpdateView;
    }



    private void UpdateView(int value)
    {
        _textComp.text = _prefix + value.ToString() + " M.";
    }



}
