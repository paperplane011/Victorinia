using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DifficultyChangeButton : MonoBehaviour
{
    [SerializeField] private bool _isRightButton;
    [SerializeField] private DifficultyView _difficultyView;


    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }


    private void Clicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.ClickSwitch);

        if (_isRightButton)
        {
            _difficultyView.GoRight();
        }
        else
        {
            _difficultyView.GoLeft();
        }

    }

}
