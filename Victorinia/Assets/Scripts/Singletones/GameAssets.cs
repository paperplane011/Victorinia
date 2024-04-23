using System;
using Tweens;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region SINGLETONE
    private static GameAssets _instance;
    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return _instance;

        }
    }
    #endregion 


    public const string TOPIC_VIEW_TAG = "TopicView";

    [Header("Questions")]
    [SerializeField] private QuestionList[] _questionLists;

    


    public QuestionList GetQuestionListForDifficulty(QuestionDifficulty questionDifficulty)
    {
        foreach(var questionList in _questionLists)
        {
            if(questionList.ThisQuestionListDifficulty == questionDifficulty)
            {
                return questionList;
            }
        }

        throw new ArgumentException(nameof(questionDifficulty));
    }


    public TopicView GetTopicView()
    {
        TopicView topicView = GameObject.FindGameObjectWithTag(TOPIC_VIEW_TAG).GetComponent<TopicView>();
        return topicView;
    }



    public SoundManager.SoundInfo[] SoundInfoArray;

    

}
