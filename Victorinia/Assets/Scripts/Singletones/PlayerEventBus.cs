using System;
using UnityEngine;

public static class PlayerEventBus
{
    

    [Serializable]
    public enum EventType
    {
        OnDifficultySelected,
        OnAnswerPressed,
        OnGameEndLose,
        OnGameEndWin,
        OnToMainMenuPressed,
        OnRestartPressed,
        OnToSelectMenuPressed,
        OnToPauseMenuPressed,
        OnToPauseMenuUnpressed,
        OnTopicViewSet
    }

    public static Action<QuestionDifficulty> OnDifficultySelected;
    public static Action<Topic, QuestionDifficulty> OnStartGame;
    public static Action<bool> OnAnswerPressed; // Invoked by AnswerButton.cs

    public static Action OnUpdateTopicViewVisuals;
    public static Action<int> OnMoneyChanged;





    public static Action OnGameEndLose;
    public static Action OnGameEndWin;

    public static Action OnToMainMenuPressed;
    public static Action OnRestartPressed;
    public static Action OnToSelectMenuPressed;

    public static Action OnToPauseMenuPressed;
    public static Action OnToPauseMenuUnpressed;

    
    public static void RaiseEvent(EventType eventType, params object[] args)
    {
        switch (eventType)
        {
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
