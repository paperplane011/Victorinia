using Tweens;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GameScreenColorTween : MonoBehaviour
{

    private Image _image;


    [SerializeField] private float _transDuration;
    [SerializeField] private bool _doUnsubscribeOnDisable = true;
    [Header("Colors")]
    [SerializeField] private Color _mainMenuColor;
    [SerializeField] private Color _selectMenuColor;
    [SerializeField] private Color _questionsMenuColor;
    [SerializeField] private Color _loseMenuColor;
    [SerializeField] private Color _winMenuColor;

    

    private void Awake()
    {
        _image = GetComponent<Image>();
        ScreenHandler.OnMenuShowed += TweenToColorCorrespondingToGameScreen;
    }


    private void OnDisable()
    {
        if(_doUnsubscribeOnDisable)
        ScreenHandler.OnMenuShowed -= TweenToColorCorrespondingToGameScreen;
    }


    private void TweenToColorCorrespondingToGameScreen(GameScreen gameScreen)
    {
        Color tweenToColor = GetColorCorrespondingToGameScreen(gameScreen);

        Tweens.ColorTween transTween = new Tweens.ColorTween
        {
            from = _image.color,
            to = tweenToColor,
            duration = _transDuration,
            easeType = EaseType.QuadInOut,
            onUpdate = (t, c) => _image.color = c,
        };

        gameObject.AddTween(transTween);

    }


    private Color GetColorCorrespondingToGameScreen(GameScreen gameScreen)
    {
        Color color = _image.color;

        switch (gameScreen)
        {
            case GameScreen.Title:
                color = _mainMenuColor;
                break;
            case GameScreen.Select:
                color = _selectMenuColor;
                break;
            case GameScreen.Lose:
                color = _loseMenuColor;
                break;
            case GameScreen.Win:
                color = _winMenuColor;
                break;
            case GameScreen.Questions:
                color = _questionsMenuColor;
                break;
        }

        return color;
    }


}
