using UnityEngine;

public class PlayerDataInitializer : MonoBehaviour
{


    private void Awake()
    {
        SoundManager.Initialize();
    }


    private void Start()
    {

        PlayerData.Initialize();

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

    [ContextMenu("Show Info")]
    public void ShowInfo()
    {
        PlayerData.ShowInfo();
    }

}
