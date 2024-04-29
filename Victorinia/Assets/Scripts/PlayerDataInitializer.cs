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
            _isInitialized = true;
        }

    }

    [ContextMenu("ResetProgress")]
    public void ResetProgress()
    {
        PlayerData.ResetProgress();
    }


}
