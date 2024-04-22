using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DifficultySetButton : MonoBehaviour
{
    [SerializeField] private QuestionDifficulty _selectableDifficulty;

    public QuestionDifficulty SelectableDifficulty { get { return _selectableDifficulty; } }

    private Button _button;
    private TextMeshProUGUI _textComp;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _textComp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);
        _textComp.text = _selectableDifficulty.ToString();
        
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void Clicked()
    {
        PlayerEventBus.SelectedDifficulty = _selectableDifficulty;
        PlayerEventBus.OnPlayerDifficultySelected?.Invoke(_selectableDifficulty);
    }

}
