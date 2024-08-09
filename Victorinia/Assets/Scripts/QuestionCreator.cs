using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCreator : MonoBehaviour
{

    private List<Question> _currentQuestionList;


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
        //if (topic == null) Debug.Log("TOPIC IS NULL");
        //if (topic.GetQuestionListForDifficulty(questionDifficulty).ThisQuestionList.Count == 0) Debug.Log("Question list is empty :(");

        //if (topic.GetQuestionListForDifficulty(questionDifficulty) == null) Debug.Log("No question list");

        _currentQuestionList = new();
        _currentQuestionList.AddRange(topic.GetQuestionListForDifficulty(questionDifficulty).ThisQuestionList); // copying obj
        
        //Debug.Log("current question list count: " + _currentQuestionList.Count);

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
        int randomQuestionIndex = UnityEngine.Random.Range(0, _currentQuestionList.Count);
        Question randomQuestion = _currentQuestionList[randomQuestionIndex];
        // Remove used question
        _currentQuestionList.RemoveAt(randomQuestionIndex);

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

            if (_currentQuestionList.Count == 0) // win behaviour
            {
                PlayerData.SetRewardToZero(_currentTopic.ID, _currentQuestionDifficulty);
                PlayerData.SetDifficultyAsCompleted(_currentTopic.ID, _currentQuestionDifficulty);

                SoundManager.PlaySound(SoundManager.Sound.WinEffect);

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
