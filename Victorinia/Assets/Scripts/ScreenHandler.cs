using System;
using System.Collections.Generic;
using UnityEngine;


public enum GameScreen
{
    Title,
    Select,
    Questions,
    Lose,
    Win,
    Pause
}

public class ScreenHandler : MonoBehaviour
{

    [SerializeField] private GameScreenCanvasGroup[] _gameScreenCanvasGroupArray; // used to assign values in inspector
    private Dictionary<GameScreen, CanvasGroup> _gameScreenCanvasGroupDictionary; // used for working purposes

   
    private Stack<GameScreen> _currentScreenStack;
    private readonly GameScreen STARTING_SCREEN = GameScreen.Title;


    private void Awake()
    {
        InitializeGameScreenCanvasGroupDictionary();
        
    }

    private void InitializeGameScreenCanvasGroupDictionary()
    {
        _gameScreenCanvasGroupDictionary = new();

        foreach (var item in _gameScreenCanvasGroupArray)
        {
            _gameScreenCanvasGroupDictionary.Add(item.GameScreen, item.CanvasGroup);
        }
    }

    private void OnEnable()
    {
        PlayerEventsInvoker.OnPlayerDifficultySelectedFinally += (QuestionDifficulty d) => ChangeScreenTo(GameScreen.Questions);
        PlayerEventsInvoker.OnGameEndLose += () => ChangeScreenTo(GameScreen.Lose);
        PlayerEventsInvoker.OnGameEndWin += () => ChangeScreenTo(GameScreen.Win);
        PlayerEventsInvoker.OnToMainMenuPressed += () => ChangeScreenTo(GameScreen.Title);
        PlayerEventsInvoker.OnRestartPressed += () => ChangeScreenTo(GameScreen.Questions);
        PlayerEventsInvoker.OnToSelectMenuPressed += () => ChangeScreenTo(GameScreen.Select);
        PlayerEventsInvoker.OnToPauseMenuPressed += () => ChangeScreenTo(GameScreen.Pause, true);
        PlayerEventsInvoker.OnToPauseMenuUnpressed += () => HideCurrentScreen();
    }

    private void OnDisable()
    {
        PlayerEventsInvoker.OnPlayerDifficultySelectedFinally -= (QuestionDifficulty d) => ChangeScreenTo(GameScreen.Questions);
        PlayerEventsInvoker.OnGameEndLose -= () => ChangeScreenTo(GameScreen.Lose);
        PlayerEventsInvoker.OnGameEndWin -= () => ChangeScreenTo(GameScreen.Win);

        PlayerEventsInvoker.OnToMainMenuPressed -= () => ChangeScreenTo(GameScreen.Title);
        PlayerEventsInvoker.OnRestartPressed -= () => ChangeScreenTo(GameScreen.Questions);
        PlayerEventsInvoker.OnToSelectMenuPressed -= () => ChangeScreenTo(GameScreen.Select);

        PlayerEventsInvoker.OnToPauseMenuPressed -= () => ChangeScreenTo(GameScreen.Pause, true);
        PlayerEventsInvoker.OnToPauseMenuUnpressed -= () => HideCurrentScreen();

    }


    private void Start()
    {
        HideAllScreens();

        _currentScreenStack = new();

        _currentScreenStack.Push(STARTING_SCREEN);
        ChangeScreenTo(STARTING_SCREEN);
        
    }





    public void ChangeScreenTo(GameScreen newScreen, bool isOverlap = false)
    {

        if(!isOverlap)
        {
            HideAllScreens();            
            _currentScreenStack.Pop();
        }

        CanvasUtils.EnableCanvasGroup(_gameScreenCanvasGroupDictionary[newScreen]);
        _currentScreenStack.Push(newScreen);
        
    }

    public void HideCurrentScreen()
    {
        CanvasUtils.DisableCanvasGroup(_gameScreenCanvasGroupDictionary[_currentScreenStack.Peek()]) ;
        _currentScreenStack.Pop();
    }


    private void HideAllScreens()
    {
        foreach(var item in _gameScreenCanvasGroupArray)
        {
            CanvasUtils.DisableCanvasGroup(item.CanvasGroup);
        }
    }

}

[Serializable]
public class GameScreenCanvasGroup
{
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private CanvasGroup _canvasGroup;

    public GameScreen GameScreen { get { return _gameScreen; } }
    public CanvasGroup CanvasGroup { get { return _canvasGroup; } }
}