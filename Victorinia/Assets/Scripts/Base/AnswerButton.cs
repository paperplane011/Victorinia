using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public bool IsCorrect { get; private set; }

    [Header("Component hooks")]
    [SerializeField] private TMPro.TextMeshProUGUI _textComponent;

    public void AssignAnswer(Answer answer)
    {
        _textComponent.text = answer.AnswerText;
        IsCorrect = answer.IsCorrect;
        
    }





}
