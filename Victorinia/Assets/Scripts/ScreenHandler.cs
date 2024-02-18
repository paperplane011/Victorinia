using System;
using System.Collections.Generic;
using UnityEngine;


public enum GameScreen
{
    Title,
    Select,
    Questions,
    Lose,
    Win
}

public class ScreenHandler : MonoBehaviour
{

    [SerializeField] private GameScreenCanvasGroup[] _gameScreenCanvasGroupArray;
    private Dictionary<GameScreen, CanvasGroup> _gameScreenCanvasGroupDictionary;


    private GameScreen _currentScreen;
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
        PlayerEventsInvoker.OnGameEndLose += () => ChangeScreenTo(GameScreen.Lose);
        PlayerEventsInvoker.OnGameEndWin += () => ChangeScreenTo(GameScreen.Win);
    }

    private void OnDisable()
    {
        PlayerEventsInvoker.OnGameEndLose -= () => ChangeScreenTo(GameScreen.Lose);
        PlayerEventsInvoker.OnGameEndWin -= () => ChangeScreenTo(GameScreen.Win);
    }


    private void Start()
    {
        ChangeScreenTo(STARTING_SCREEN);
        _currentScreen = STARTING_SCREEN;
    }

    public void ChangeScreenTo(GameScreen newScreen)
    {
        CanvasUtils.DisableCanvasGroup(_gameScreenCanvasGroupDictionary[_currentScreen]);
        CanvasUtils.EnableCanvasGroup(_gameScreenCanvasGroupDictionary[newScreen]);

        _currentScreen = newScreen;
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