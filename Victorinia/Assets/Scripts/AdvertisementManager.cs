using UnityEngine;
using YG;


public class AdvertisementManager : MonoBehaviour
{

    private void OnEnable()
    {
        MenusHandler.OnMenuShowed += ShowFullscreen;
    }

    private void OnDisable()
    {
        MenusHandler.OnMenuShowed -= ShowFullscreen;
    }



    private void ShowFullscreen(GameScreen gameScreen)
    {
        if(gameScreen == GameScreen.Lose || gameScreen == GameScreen.Select)
        {
            YandexGame.FullscreenShow();
        }

    }


}
