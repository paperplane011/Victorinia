using System;
using System.Collections;
using UnityEngine;

public class QuestionCreator : MonoBehaviour
{

    private QuestionList _currentQuestionList;


    private Topic _currentTopic;
    private QuestionDifficulty _currentQuestionDifficulty;

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
        PlayerEventBus.OnStartGame += GameStartBehaviour;
        PlayerEventBus.OnAnswerPressed += AnswerPressedBehaviour;

        PlayerEventBus.OnRestartPressed += RestartBehaviour;
    }

    private void OnDisable()
    {
        PlayerEventBus.OnStartGame -= GameStartBehaviour;
        PlayerEventBus.OnAnswerPressed -= AnswerPressedBehaviour;

        PlayerEventBus.OnRestartPressed -= RestartBehaviour;
    }

    private void AnswerPressedBehaviour(bool isCorrect)
    {
        StartCoroutine(CheckForAnswerWithDelay(isCorrect));

    }

    private void RestartBehaviour()
    {
        GameStartBehaviour(_currentTopic,_currentQuestionDifficulty);
    }


    private void GameStartBehaviour(Topic topic, QuestionDifficulty questionDifficulty)
    {
        _currentQuestionList = Instantiate(topic.GetQuestionListForDifficulty(questionDifficulty)); // copying obj

        _currentTopic = topic;
        _currentQuestionDifficulty = questionDifficulty;
        PlayerEventBus.OnRewardSetted?.Invoke(topic.GetRewardForDifficulty(questionDifficulty));

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


        SetAnswersForQuestion(randomQuestion);
    }


    private void SetAnswersForQuestion(Question question)
    {
        // Set answers
        ClearAnswerButtons(); // disable all answers 
        ShuffleAnswerButtonsArray(); // for random answer placement

        int numOfAnswers = question.AnswerArray.Length;

        for (int i = 0; i < numOfAnswers; i++)
        {
            _answerButtonsArray[i].SetActive(true);
            _answerButtonsArray[i].GetComponent<AnswerButton>().AssignAnswer(question.AnswerArray[i]);
        }
    }


    IEnumerator CheckForAnswerWithDelay(bool isCorrect)
    {
        if (isCorrect)
        {
            
            yield return new WaitForSeconds(0.4f);

            if (_currentQuestionList.ThisQuestionList.Count == 0) // win behaviour
            {
                PlayerData.SetRewardToZero(_currentTopic.ID, _currentQuestionDifficulty);

                PlayerEventBus.OnGameEndWin?.Invoke();
            }
            else
            {
                CreateQuestion();
               
            }
        }
        else // lose behaviour
        {
            
            yield return new WaitForSeconds(1.3f);

            PlayerEventBus.OnGameEndLose?.Invoke();
        }

        BlockRaycastScreen.SetBlockRaycastStatus?.Invoke(false);
    }
}
