using System.Collections.Generic;
using UnityEngine;

public class QuestionCreator : MonoBehaviour
{

    private QuestionList _currentQuestionList;
    private List<GameObject> _answerButtonsGOList;

    [Header("Component hooks")]
    [SerializeField] private GameObject _answerButtonsParentGO;
    [SerializeField] private TMPro.TextMeshProUGUI _questionTextComp;


    private void Start()
    {
        _answerButtonsGOList = new();

        for (int i = 0; i < _answerButtonsParentGO.transform.childCount; i++)
        {
            _answerButtonsGOList.Add(_answerButtonsParentGO.transform.GetChild(i).gameObject);
            
        }

        ClearAnswerButtons();

    }

    private void OnEnable()
    {
        PlayerEventsInvoker.OnPlayerDifficultySelected += GameStartBehaviour;
        PlayerEventsInvoker.OnAnswerPressed += AnswerPressedBehaviour;

        PlayerEventsInvoker.OnRestartPressed += RestartBehaviour;
    }

    private void OnDisable()
    {
        PlayerEventsInvoker.OnPlayerDifficultySelected -= GameStartBehaviour;
        PlayerEventsInvoker.OnAnswerPressed -= AnswerPressedBehaviour;

        PlayerEventsInvoker.OnRestartPressed -= RestartBehaviour;
    }

    private void AnswerPressedBehaviour(bool isCorrect)
    {
        if (isCorrect)
        {
            if(_currentQuestionList.ThisQuestionList.Count == 0)
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
            PlayerEventsInvoker.OnGameEndLose?.Invoke();
        }
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
        foreach (var answerButtonGO in _answerButtonsGOList)
        {
            answerButtonGO.SetActive(false);
        }
    }



    [ContextMenu("Create")]
    public void CreateQuestion()
    {
        if (_currentQuestionList == null) return;

        // Get question
        int randomQuestionIndex = Random.Range(0, _currentQuestionList.ThisQuestionList.Count);
        Question randomQuestion = _currentQuestionList.ThisQuestionList[randomQuestionIndex];
        // Remove used question
        _currentQuestionList.ThisQuestionList.RemoveAt(randomQuestionIndex);

        // Set question text
        _questionTextComp.text = randomQuestion.QuestionText;

        // Set answers
        ClearAnswerButtons();
        int numOfAnswers = randomQuestion.AnswerArray.Length;

        for (int i = 0; i < numOfAnswers; i++)
        {
            _answerButtonsGOList[i].SetActive(true);
            _answerButtonsGOList[i].GetComponent<AnswerButton>().AssignAnswer(randomQuestion.AnswerArray[i]);
        }
    }



}
