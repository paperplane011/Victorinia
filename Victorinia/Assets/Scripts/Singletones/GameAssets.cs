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
    private TopicView _topicView;



    public TopicView GetTopicView()
    {
        if (_topicView == null)
        {
            _topicView = GameObject.FindGameObjectWithTag(TOPIC_VIEW_TAG).GetComponent<TopicView>();
        }

        return _topicView;
    }


    [SerializeField] private TopicSaveToJsonAsset _topicSaveToJsonAsset;

    public TopicSaveToJsonAsset TopicSaveToJsonAsset { get { return _topicSaveToJsonAsset; } }


    public SoundManager.SoundInfo[] SoundInfoArray;







}
