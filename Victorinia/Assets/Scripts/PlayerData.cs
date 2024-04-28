using System.Collections.Generic;
using UnityEngine;
using YG;

public static class PlayerData
{

    private static int _money;
    private static List<string> _topicSaveJSONList;
    private static Dictionary<int, TopicSave> _idToTopicSaveDictionary;

    public static int Money { get { return _money; } }


    public static void Initialize()
    {
        //_money = 100;
       // PlayerEventBus.OnMoneyChanged(_money);

        InitializeData();
    }


    private static void InitializeData()
    {

        _topicSaveJSONList = new();

        if (YandexGame.savesData.TopicSaveJSONList.Count == 0)
        {
            InitializeFromGameAssets();
            Debug.Log("from game assets");
        }
        else
        {
            InitializeFromSavesYG();
            Debug.Log("from yg");
        }

        FillDictionary();

    }


    private static void InitializeFromGameAssets()
    {
        _money = 0;
        _topicSaveJSONList = GameAssets.Instance.TopicSaveJSONList;

    }

    private static void InitializeFromSavesYG()
    {
        _money = YandexGame.savesData.Money;
        _topicSaveJSONList = YandexGame.savesData.TopicSaveJSONList;
    }

    private static void FillDictionary()
    {
        _idToTopicSaveDictionary = new();
        _idToTopicSaveDictionary.Clear();

        foreach(string topicSaveJSON in _topicSaveJSONList)
        {
            TopicSave topicSave = JsonUtility.FromJson<TopicSave>(topicSaveJSON);
            _idToTopicSaveDictionary.Add(topicSave.ID, topicSave);
        }


    }

    private static void SaveDictionaryToSavesYG()
    {
        List<string> newSaveList = new();

        foreach(var elem in _idToTopicSaveDictionary)
        {
            newSaveList.Add(JsonUtility.ToJson(elem.Value));
        }

        YandexGame.savesData.TopicSaveJSONList = newSaveList;
        YandexGame.SaveProgress();
    }


    public static bool TryToChangeMoney(int value)
    {
        if (value < 0 && _money + value > 0)
        {
            _money += value;
            PlayerEventBus.OnMoneyChanged(_money);

            YandexGame.savesData.Money = _money;
            YandexGame.SaveProgress();

            return true;
            
        }
        else
        {
            return false;
        }
    }

    public static void SaveTopic(Topic topic)
    {

        TopicSave newTopicSave = topic.ToTopicSave();
        _idToTopicSaveDictionary[topic.ID] = newTopicSave;

        SaveDictionaryToSavesYG();
    }




}
