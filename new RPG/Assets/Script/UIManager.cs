using UnityEngine;
using UnityEngine.UI;

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
    public GameObject hudUI;
    public GameObject pickupHint;
    public Text pickupHintTitle;
    public Text pickupHintCaption; 


    void Start()
    {
        gameMenuUI.SetActive(false);
        inventoryUI.SetActive(false);
        pickupHint.SetActive(false);
        //hudUI.SetActive(false);
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

    public void showPickupHint(string title, string Caption, Color titleColor, bool setToHidden = false)
    {
        if(setToHidden && pickupHint.activeSelf)
        {
            pickupHint.SetActive(false);
        }
        else
        {
            pickupHintTitle.text = title;
            pickupHintTitle.color = titleColor;
            pickupHintCaption.text = Caption;
            if (!pickupHint.activeSelf) pickupHint.SetActive(true);
        }
    }

}
