using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DifficultySetButton : MonoBehaviour
{
    [SerializeField] private QuestionDifficulty _selectableDifficulty;

    public QuestionDifficulty SelectableDifficulty { get { return _selectableDifficulty; } }

    private TextMeshProUGUI _text;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _text.text = _selectableDifficulty.ToString();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SetDifficulty);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void SetDifficulty()
    {
        PlayerEventsInvoker.SelectedDifficulty = _selectableDifficulty;
        PlayerEventsInvoker.OnPlayerDifficultySelected?.Invoke(_selectableDifficulty);
    }

}
