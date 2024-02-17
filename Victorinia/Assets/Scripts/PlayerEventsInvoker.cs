using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEventsInvoker
{

    public static Action<QuestionDifficulty> OnPlayerDifficultySelected;
    public static Action<bool> OnAnswerPressed;

    public static Action OnGameEndLose;
    public static Action OnGameEndWin;


    private static QuestionDifficulty SelectedDifficulty { get; set; }




    public static void InvokeOnPlayerDifficultySelected()
    {
        OnPlayerDifficultySelected?.Invoke(SelectedDifficulty);
    }



}
