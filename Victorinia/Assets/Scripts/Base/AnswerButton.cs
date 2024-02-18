using System;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{

    public bool IsCorrect { get; private set; }

    [Header("Component hooks")]
    [SerializeField] private TMPro.TextMeshProUGUI _textComponent;
    private Button _button;


    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }


    public void AssignAnswer(Answer answer)
    {
        _textComponent.text = answer.AnswerText;
        IsCorrect = answer.IsCorrect;
    }


    public void Clicked()
    {
        if (IsCorrect)
        {
            PlayerEventsInvoker.OnAnswerPressed?.Invoke(true);
        }
        else
        {
            PlayerEventsInvoker.OnAnswerPressed?.Invoke(false);
        }
    }





}
