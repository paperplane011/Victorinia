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

    [SerializeField] private Color _questionsMenuColor1;
    [SerializeField] private Color _questionsMenuColor2;
    [SerializeField] private Color _questionsMenuColor3;
    [SerializeField] private Color _questionsMenuColor4;
    [SerializeField] private Color _questionsMenuColor5;
    [SerializeField] private Color _questionsMenuColor6;
    [SerializeField] private Color _questionsMenuColor7;


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
                color = GetRandomQuestionsMenuColor();
                break;
        }

        return color;
    }

    private Color GetRandomQuestionsMenuColor()
    {
        Random.InitState(Time.frameCount); 
        int num = Random.Range(1, 8);

        switch (num)
        {
            case 1: return _questionsMenuColor1;
            case 2: return _questionsMenuColor2;
            case 3: return _questionsMenuColor3;
            case 4: return _questionsMenuColor4;
            case 5: return _questionsMenuColor5;
            case 6: return _questionsMenuColor6;
            case 7: return _questionsMenuColor7;
            default: return _questionsMenuColor1;
        }



    }


}
