using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CreateAssetMenu(menuName = "TopicSaveToJsonAsset")]
public class TopicSaveToJsonAsset : ScriptableObject
{


    [SerializeField] private List<string> _topicSaveJSONList; // initial data

    public List<string> TopicSaveJSONList { get { return _topicSaveJSONList; } }




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

            topic.FillTopic();
            topic.ID = i;
            Debug.Log("ID: " + i + ". Topic: " + topic.Caption);
            i++;

            _topicSaveJSONList.Add(JsonUtility.ToJson(topic.ToTopicSave()));
            
        }
    }



#endif
}
