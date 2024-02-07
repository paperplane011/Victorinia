using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question")]
public class Question : ScriptableObject
{
    [SerializeField] private string _questionText;
    [SerializeField] private AnswerItem[] _answerItemArray;


    public string QuestionText { get { return _questionText; } }
    public AnswerItem[] AnswerItemArray { get { return _answerItemArray; } }




    [Serializable]
    public class AnswerItem
    {
        [SerializeField] private string _answerText;
        [SerializeField] private bool _isCorrect;

        public bool IsCorrect { get { return _isCorrect; } }
    }

   

}



