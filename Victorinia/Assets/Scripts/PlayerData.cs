using System;
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
        InitializeData();
    }


    private static void InitializeData()
    {

        if (!YandexGame.savesData.IsInitialized)
        {
            InitializeFromGameAssets();
            YandexGame.savesData.IsInitialized = true;
            YandexGame.SaveProgress();
            Debug.Log("from game assets");
        }
        else
        {
            InitializeFromSavesYG();
            Debug.Log("from yg");
        }

        FillDictionary();

        PlayerEventBus.OnMoneyChanged?.Invoke(_money, 0);
        PlayerEventBus.OnUpdateTopicViewVisuals?.Invoke();
        SaveDictionaryToSavesYG();

    }


    private static void InitializeFromGameAssets()
    {
        _money = 0;

        _topicSaveJSONList = new();
        _topicSaveJSONList.AddRange(GameAssets.Instance.TopicSaveToJsonAsset.TopicSaveJSONList);

    }

    private static void InitializeFromSavesYG()
    {
        _money = YandexGame.savesData.Money;

        _topicSaveJSONList = new();
        _topicSaveJSONList.AddRange(YandexGame.savesData.TopicSaveJSONList);
    }

    private static void FillDictionary()
    {
        _idToTopicSaveDictionary = new();
        _idToTopicSaveDictionary.Clear();

        foreach (string topicSaveJSON in _topicSaveJSONList)
        {
            TopicSave topicSave = JsonUtility.FromJson<TopicSave>(topicSaveJSON);
            _idToTopicSaveDictionary.Add(topicSave.ID, topicSave);
        }


    }

    public static void ResetProgress()
    {
        
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
        InitializeData();
    }

    private static void SaveDictionaryToSavesYG()
    {
        List<string> newSaveList = new();

        foreach (var elem in _idToTopicSaveDictionary)
        {
            newSaveList.Add(JsonUtility.ToJson(elem.Value));
        }

        YandexGame.savesData.TopicSaveJSONList = newSaveList;
        YandexGame.SaveProgress();
    }


    public static void ShowInfo()
    {
        foreach(var elem in _idToTopicSaveDictionary)
        {
            Debug.Log(elem.Key + ":\nEasy completed: " + PlayerData.IsTopicDifficultyCompleted(elem.Key, QuestionDifficulty.Easy) + "\nNormal completed: " + PlayerData.IsTopicDifficultyCompleted(elem.Key, QuestionDifficulty.Normal) + "\nHard completed: " + PlayerData.IsTopicDifficultyCompleted(elem.Key, QuestionDifficulty.Hard));
        }
    }


    public static bool TryToChangeMoney(int value)
    {
        if (_money + value > 0)
        {

            _money += value;
            SoundManager.PlaySound(SoundManager.Sound.MoneyChange);

            PlayerEventBus.OnMoneyChanged(_money, value);

            YandexGame.savesData.Money = _money;
            YandexGame.SaveProgress();

            return true;
        }
        else
        {
            return false;
        }
    }

    


    public static void UnlockDifficulty(int topicID, QuestionDifficulty questionDifficulty) // changes topic
    {
        TopicSave topicSave = _idToTopicSaveDictionary[topicID];
        

        foreach(var elem in topicSave.QuestionDifficultyToLockedStatusArray)
        {
            if(elem.QuestionDifficulty == questionDifficulty)
            {
                elem.BoolValue = false;
            }
        }

        SaveDictionaryToSavesYG();


    }

    public static void SetRewardToZero(int topicID, QuestionDifficulty questionDifficulty) // changes topic
    {
        TopicSave topicSave = _idToTopicSaveDictionary[topicID];


        foreach (var elem in topicSave.QuestionDifficultyToRewardArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                elem.IntValue = 0;
                PlayerEventBus.OnUpdateTopicViewVisuals?.Invoke();
            }
        }

        SaveDictionaryToSavesYG();

    }

    public static void SetDifficultyAsCompleted(int topicID, QuestionDifficulty questionDifficulty)
    {
        TopicSave topicSave = _idToTopicSaveDictionary[topicID];

        foreach(var elem in topicSave.QuestionDifficultyToCompletedStatusArray)
        {
            if(elem.QuestionDifficulty == questionDifficulty)
            {
                elem.BoolValue = true;
            }
        }

        SaveDictionaryToSavesYG();

    }

    public static bool IsTopicDifficultyCompleted(int topicID, QuestionDifficulty questionDifficulty)
    {
        TopicSave topicSave = _idToTopicSaveDictionary[topicID];

        foreach (var elem in topicSave.QuestionDifficultyToCompletedStatusArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.BoolValue;
            }
        }

        throw new ArgumentException();
    }

    public static int GetNumOfCompletedDifficultiesOfTopic(int topicID)
    {
        TopicSave topicSave = _idToTopicSaveDictionary[topicID];

        int num = 0;
        
        for(int i=0; i< Topic.MAX_NUM_OF_DIFFICULTIES; i++)
        {
            if (topicSave.QuestionDifficultyToCompletedStatusArray[i].BoolValue)
            {
                num++;
            }
        }
        return num;
    }


    public static bool IsTopicDifficultyLocked(int topicID, QuestionDifficulty questionDifficulty)
    {
        TopicSave topicSave = _idToTopicSaveDictionary[topicID];

        foreach (var elem in topicSave.QuestionDifficultyToLockedStatusArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.BoolValue;
            }
        }

        throw new ArgumentException();

    }




    public static int GetTopicRewardForDifficulty(int topicID, QuestionDifficulty questionDifficulty)
    {
        TopicSave topicSave = _idToTopicSaveDictionary[topicID];

        foreach (var elem in topicSave.QuestionDifficultyToRewardArray)
        {
            if (elem.QuestionDifficulty == questionDifficulty)
            {
                return elem.IntValue;
            }
        }

        throw new ArgumentException();
    }






}
