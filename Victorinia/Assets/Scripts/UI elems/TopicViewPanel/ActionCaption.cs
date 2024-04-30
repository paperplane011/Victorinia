using UnityEngine;

public class ActionCaption : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI _textComp;
    [SerializeField] private string _costPrefix;
    [SerializeField] private string _rewardPrefix;
    [SerializeField] private string _noMoneyMessage;

    public enum State
    {
        cost,
        reward,
        noMoney
    }

    public void ChangeToState(State state, int value)
    {

        switch (state)
        {
            case State.cost:
                _textComp.text = _costPrefix + value.ToString();
                break;
            case State.reward:
                _textComp.text = _rewardPrefix + value.ToString();
                break;
            case State.noMoney:
                _textComp.text = _noMoneyMessage + value.ToString(); // value equals to money lacking 
                break;
        }



    }







}
