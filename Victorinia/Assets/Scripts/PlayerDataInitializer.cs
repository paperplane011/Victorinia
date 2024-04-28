using UnityEngine;

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

}
