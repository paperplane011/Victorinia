using UnityEngine;
using YG;

public class PlayerDataInitializer : MonoBehaviour
{
    private static bool _isInitialized = false;

    private void Start()
    {
        if (!_isInitialized)
        {
            PlayerData.Initialize();
            SoundManager.Initialize();
            _isInitialized = true;
        }

    }

    [ContextMenu("ResetProgress")]
    public void ResetProgress()
    {
        PlayerData.ResetProgress();
    }

    [ContextMenu("Add money")]
    public void AddMoney()
    {
        PlayerData.TryToChangeMoney(20);
    }


    [ContextMenu("Substract money")]
    public void SubstractMoney()
    {
        PlayerData.TryToChangeMoney(-20);
    }

}
