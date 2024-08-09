using UnityEngine;

public class GameAssets : MonoBehaviour
{
   
    private static GameAssets _instance;
    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("_instance == null");
                _instance = GameObject.FindGameObjectWithTag("GameAssets").GetComponent<GameAssets>();
            }

            Debug.Log("_instance exist");
            return _instance;

        }
    }

    public void Awake()
    {
        Debug.Log("!!!! GAME ASSETS INSTANTIATED !!!!");
    }


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
