using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Question")]
public class Question : ScriptableObject
{
    [SerializeField] private QuestionDifficulty _questionDifficulty;
    [SerializeField] private string _questionText;
    [SerializeField] private bool _isMultipleAnswers;
    [SerializeField] private Answer[] _answerArray;



    public string QuestionText { get { return _questionText; } }
    public Answer[] AnswerArray { get { return _answerArray; } }
    public bool IsMultipleAnswers { get { return _isMultipleAnswers; } }
    public QuestionDifficulty QuestionDifficulty { get { return _questionDifficulty; } }

}


[Serializable]
public class Answer
{
    [SerializeField] private string _answerText;
    [SerializeField] private bool _isCorrect;

    public string AnswerText { get { return _answerText; } }
    public bool IsCorrect { get { return _isCorrect; } }
}


[Serializable]
public enum QuestionDifficulty
{
    Easy,
    Normal,
    Hard
}


