using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCreator : MonoBehaviour
{

    private QuestionList _currentQuestionList;

    private List<GameObject> _answerButtonsGOList;

    [Header("Component hooks")]
    [SerializeField] private GridLayoutGroup _answerButtonsGrid;
    [SerializeField] private GameObject _answerButtonsParentGO;


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
    }

    private void OnDisable()
    {
        PlayerEventsInvoker.OnPlayerDifficultySelected -= GameStartBehaviour;
    }


    private void ClearAnswerButtons()
    {
        foreach (var answerButtonGO in _answerButtonsGOList)
        {
            answerButtonGO.SetActive(false);
        }
    }



    private void GameStartBehaviour(QuestionDifficulty questionDifficulty)
    {
        _currentQuestionList = GameAssets.Instance.GetQuestionListForDifficulty(questionDifficulty);
        CreateQuestion();
    }



    [ContextMenu("Create")]
    public void CreateQuestion()
    {
        _currentQuestionList = GameAssets.Instance.GetQuestionListForDifficulty(QuestionDifficulty.Easy);

        if (_currentQuestionList == null) return;

        Question randomQuestion = _currentQuestionList.ThisQuestionList[Random.Range(0, _currentQuestionList.ThisQuestionList.Count)];

        int numOfAnswers = randomQuestion.AnswerArray.Length;

        ClearAnswerButtons();

        for (int i = 0; i < numOfAnswers; i++)
        {
            _answerButtonsGOList[i].SetActive(true);
        }



    }



}
