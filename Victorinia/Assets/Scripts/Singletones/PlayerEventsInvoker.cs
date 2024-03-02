using System;
using UnityEngine;

public static class PlayerEventsInvoker
{
    [Serializable]
    public enum EventType
    {
        OnPlayerDifficultySelected,
        OnAnswerPressed,
        OnGameEndLose,
        OnGameEndWin,
        OnToMainMenuPressed,
        OnRestartPressed,
        OnToSelectMenuPressed,
        OnToPauseMenuPressed,
        OnToPauseMenuUnpressed
    }

    public static Action<QuestionDifficulty> OnPlayerDifficultySelected;
    public static Action<QuestionDifficulty> OnPlayerDifficultySelectedFinally;
    public static Action<bool> OnAnswerPressed; // Invoked by AnswerButton.cs

    public static Action OnGameEndLose;
    public static Action OnGameEndWin;

    public static Action OnToMainMenuPressed;
    public static Action OnRestartPressed;
    public static Action OnToSelectMenuPressed;

    public static Action OnToPauseMenuPressed;
    public static Action OnToPauseMenuUnpressed;



    public static QuestionDifficulty SelectedDifficulty { get; set; }



    public static void RaiseEvent(EventType eventType, params object[] args)
    {
        switch (eventType)
        {
            case EventType.OnPlayerDifficultySelected:
                OnPlayerDifficultySelectedFinally?.Invoke(SelectedDifficulty);
                break;
            case EventType.OnAnswerPressed:
                OnAnswerPressed?.Invoke((bool)args[0]);
                break;
            case EventType.OnGameEndLose:
                OnGameEndLose?.Invoke();
                break;
            case EventType.OnGameEndWin:
                OnGameEndWin?.Invoke();
                break;
            case EventType.OnToMainMenuPressed:
                OnToMainMenuPressed?.Invoke();
                break;
            case EventType.OnRestartPressed:
                OnRestartPressed?.Invoke();
                break;
            case EventType.OnToSelectMenuPressed:
                OnToSelectMenuPressed?.Invoke();
                break;
            case EventType.OnToPauseMenuPressed:
                OnToPauseMenuPressed?.Invoke();
                break;
            case EventType.OnToPauseMenuUnpressed:
                OnToPauseMenuUnpressed?.Invoke();
                break;

            default:
                Debug.LogWarning($"{eventType} can't be raised via button. Or add event to player events invoker");
                break;
        }
    }





}
