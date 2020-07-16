using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one player manager instance found!");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject currentPlayer;
}
