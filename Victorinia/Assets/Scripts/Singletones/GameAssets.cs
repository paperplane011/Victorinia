using System.Collections.Generic;
using UnityEditor;
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


    private void Start()
    {
        _topicView = GameObject.FindGameObjectWithTag(TOPIC_VIEW_TAG).GetComponent<TopicView>();
    }

    public TopicView GetTopicView()
    {
        return _topicView;
    }

    private List<string> _topicSaveJSONList; // initial data

    public List<string> TopicSaveJSONList { get { return _topicSaveJSONList; } }



    public SoundManager.SoundInfo[] SoundInfoArray;

#if UNITY_EDITOR

    private const string TOPIC_SEARCH_FILTER = "t:Topic";


    [ContextMenu("Fill topics save")]
    public void FillTopicsSave()
    {
        string[] allTopicsGUIDs = AssetDatabase.FindAssets(TOPIC_SEARCH_FILTER);

        _topicSaveJSONList = new();
        _topicSaveJSONList.Clear();

        int i = 1;
        foreach (var topicGUID in allTopicsGUIDs)
        {
            string topicPath = AssetDatabase.GUIDToAssetPath(topicGUID);
            Debug.Log(topicPath);

            Topic topic = AssetDatabase.LoadAssetAtPath<Topic>(topicPath);

            topic.ID = i;
            i++;

            _topicSaveJSONList.Add(JsonUtility.ToJson(topic.ToTopicSave()));
        }
    }



#endif

}
