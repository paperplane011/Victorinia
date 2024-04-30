using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyViewReward : MoneyViewBalance
{

    protected override void OnEnable()
    {
        PlayerEventBus.OnRewardSetted += UpdateView;
    }

    protected override void OnDisable()
    {
        PlayerEventBus.OnRewardSetted -= UpdateView;
    }



}
