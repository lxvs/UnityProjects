using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    #region Singleton

    public static GameSettingsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of <color=green>" + name + "</color> found!");
            return;
        }
        instance = this;
    }

    #endregion

    public bool isGamePadConnected = false;

    private void Update()
    {
        isGamePadConnected = Input.GetJoystickNames().Length > 0;
    }
}
