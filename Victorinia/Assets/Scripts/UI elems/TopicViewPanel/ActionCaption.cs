using UnityEngine;

public class ActionCaption : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI _textComp;
    [SerializeField] private CanvasGroup _coinCG;

    [SerializeField] private string _costPrefix;
    [SerializeField] private string _rewardPrefix;
    [SerializeField] private string _noMoneyMessage;
    [SerializeField] private string _completedMessage;

    public enum State
    {
        cost,
        reward,
        noMoney,
        completed
    }

    public void ChangeToState(State state, int value)
    {

        switch (state)
        {
            case State.cost:
                CanvasUtils.EnableCanvasGroup(_coinCG);
                _textComp.text = _costPrefix + value.ToString();
                break;
            case State.reward:
                CanvasUtils.EnableCanvasGroup(_coinCG);
                _textComp.text = _rewardPrefix + value.ToString();
                break;
            case State.noMoney:
                CanvasUtils.EnableCanvasGroup(_coinCG);
                _textComp.text = _noMoneyMessage + value.ToString(); // value equals to money lacking 
                break;
            case State.completed:
                CanvasUtils.DisableCanvasGroup(_coinCG);
                _textComp.text = _completedMessage;
                break;
        }



    }







}
