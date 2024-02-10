using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEventsInvoker
{

    public static Action<QuestionDifficulty> OnPlayerDifficultySelected;

    private static QuestionDifficulty SelectedDifficulty { get; set; }


    public static void InvokeOnPlayerDifficultySelected()
    {
        OnPlayerDifficultySelected?.Invoke(SelectedDifficulty);
    }



}
