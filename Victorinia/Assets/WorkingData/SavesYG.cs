
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;



        public int Money;
        public bool IsInitialized;

        public List<string> TopicSaveJSONList;


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            Money = 0;
            TopicSaveJSONList = new();
            TopicSaveJSONList.Clear();
            IsInitialized = false;
        }
    }
}
