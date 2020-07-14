using UnityEngine;

public class UIManager : MonoBehaviour
{

    #region Singleton

    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Game Manager found!");
            return;
        }
        instance = this;
    }

    #endregion

    public GameObject gameMenuUI;
    public GameObject inventoryUI;

    void Start()
    {
        gameMenuUI.SetActive(false);
        inventoryUI.SetActive(false);
        //if (Inventory.instance.onItemsChangedCallBack != null) 
        //    Inventory.instance.onItemsChangedCallBack.Invoke();

    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (gameMenuUI.activeSelf) gameMenuUI.SetActive(false);
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (Input.GetButtonDown("Cancel"))      
        {
            if(inventoryUI.activeSelf || false)
            {
                inventoryUI.SetActive(false);

            }
            else
            {
                gameMenuUI.SetActive(!gameMenuUI.activeSelf);
            }

        }
        if (Input.GetButtonDown("Back"))        // remember CANCEL and BACK can't be the same key
        {
            if (gameMenuUI.activeSelf) gameMenuUI.SetActive(false);
            if (inventoryUI.activeSelf) inventoryUI.SetActive(false);
        }
    }



    public void ExitGame()
    {
        Application.Quit();
    }

}
