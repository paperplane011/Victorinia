using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCreator : MonoBehaviour
{

    private QuestionList _currentQuestionList;
    private GameObject[] _answerButtonsArray;

    [Header("Component hooks")]
    [SerializeField] private GameObject _answerButtonsParentGO;
    [SerializeField] private TMPro.TextMeshProUGUI _questionTextComp;


    private void Start()
    {
        _answerButtonsArray = new GameObject[_answerButtonsParentGO.transform.childCount];

        for (int i = 0; i < _answerButtonsParentGO.transform.childCount; i++)
        {
            _answerButtonsArray[i] = (_answerButtonsParentGO.transform.GetChild(i).gameObject);
        }

        ClearAnswerButtons();

    }

    private void OnEnable()
    {
        PlayerEventsInvoker.OnPlayerDifficultySelectedFinally += GameStartBehaviour;
        PlayerEventsInvoker.OnAnswerPressed += AnswerPressedBehaviour;

        PlayerEventsInvoker.OnRestartPressed += RestartBehaviour;
    }

    private void OnDisable()
    {
        PlayerEventsInvoker.OnPlayerDifficultySelectedFinally -= GameStartBehaviour;
        PlayerEventsInvoker.OnAnswerPressed -= AnswerPressedBehaviour;

        PlayerEventsInvoker.OnRestartPressed -= RestartBehaviour;
    }

    private void AnswerPressedBehaviour(bool isCorrect)
    {
        StartCoroutine(CheckForAnswerWithDelay(isCorrect));
       
    }
    private void RestartBehaviour()
    {
        GameStartBehaviour(_currentQuestionList.ThisQuestionListDifficulty);
    }
    private void GameStartBehaviour(QuestionDifficulty questionDifficulty)
    {
        _currentQuestionList = Instantiate(GameAssets.Instance.GetQuestionListForDifficulty(questionDifficulty)); // copying obj
        CreateQuestion();
    }


    private void ClearAnswerButtons()
    {
        foreach (var answerButtonGO in _answerButtonsArray)
        {
            answerButtonGO.SetActive(false);
        }
    }

    private void ShuffleAnswerButtonsArray()
    {
        ArrayShuffler.Shuffle(new System.Random(), _answerButtonsArray);
    }



    [ContextMenu("Create")]
    public void CreateQuestion()
    {
        if (_currentQuestionList == null) return;

        // Get question
        int randomQuestionIndex = UnityEngine.Random.Range(0, _currentQuestionList.ThisQuestionList.Count);
        Question randomQuestion = _currentQuestionList.ThisQuestionList[randomQuestionIndex];
        // Remove used question
        _currentQuestionList.ThisQuestionList.RemoveAt(randomQuestionIndex);

        // Set question text
        _questionTextComp.text = randomQuestion.QuestionText;

        // Set answers
        ClearAnswerButtons(); // disable all answers 
        ShuffleAnswerButtonsArray(); // for random answer placement

        int numOfAnswers = randomQuestion.AnswerArray.Length;

        for (int i = 0; i < numOfAnswers; i++)
        {
            _answerButtonsArray[i].SetActive(true);
            _answerButtonsArray[i].GetComponent<AnswerButton>().AssignAnswer(randomQuestion.AnswerArray[i]);
        }

    }


    IEnumerator CheckForAnswerWithDelay(bool isCorrect)
    {
        if (isCorrect)
        {
            yield return new WaitForSeconds(0.4f);

            if (_currentQuestionList.ThisQuestionList.Count == 0)
            {
                PlayerEventsInvoker.OnGameEndWin?.Invoke();
            }
            else
            {
                CreateQuestion();
            }
        }
        else
        {
            yield return new WaitForSeconds(1.3f);

            PlayerEventsInvoker.OnGameEndLose?.Invoke();
        }

        BlockRaycastScreen.SetBlockRaycastStatus?.Invoke(false);
    }
}
