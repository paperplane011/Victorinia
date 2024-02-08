using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question")]
public class Question : ScriptableObject
{
    [SerializeField] private QuestionDifficulty _questionDifficulty;
    [SerializeField] private string _questionText;
    [SerializeField] private bool _isMultipleAnswers;
    [SerializeField] private AnswerItem[] _answerItemArray;
    


    public string QuestionText { get { return _questionText; } }
    public AnswerItem[] AnswerItemArray { get { return _answerItemArray; } }
    public bool IsMultipleAnswers { get { return _isMultipleAnswers; } }
    public QuestionDifficulty QuestionDifficulty { get { return _questionDifficulty; } }



    [Serializable]
    public class AnswerItem
    {
        [SerializeField] private string _answerText;
        [SerializeField] private bool _isCorrect;

        public bool IsCorrect { get { return _isCorrect; } }
    }

   

}


[Serializable]
public enum QuestionDifficulty
{
    Easy,
    Normal,
    Hard
}


