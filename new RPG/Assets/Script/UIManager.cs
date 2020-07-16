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

    Image pickupHintBg;

    void Start()
    {
        pickupHintBg = pickupHint.GetComponent<Image>();
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

    public void HidePickupHint()
    {
        pickupHint.SetActive(false);
    }

    public void ShowPickupHint(string title, ItemQuality itemQuality, string caption = null)
    {
        else
        {
            pickupHintTitle.text = title;
            pickupHintTitle.color = Item.itemQualityColor[(int)itemQuality];
            pickupHintCaption.text = Caption?? "Press ";
            pickupHintCaption.color = captionColor ?? Color.white;
            pickupHintBg.color = bgColor ?? new Color(0, 0, 0, .5f);
            if (!pickupHint.activeSelf) pickupHint.SetActive(true);
        }
    }

    public void ShowPickupHint(string title, string Caption, Color? titleColor = null, Color? bgColor = null, Color? captionColor = null, bool setToHidden = false)
    {
        if(setToHidden && pickupHint.activeSelf)
        {
            pickupHint.SetActive(false);
        }
        else
        {
            pickupHintTitle.text = title;
            pickupHintTitle.color = titleColor ?? Color.white;
            pickupHintCaption.text = Caption;
            pickupHintCaption.color = captionColor ?? Color.white;
            pickupHintBg.color = bgColor ?? new Color(0, 0, 0, .5f);
            if (!pickupHint.activeSelf) pickupHint.SetActive(true);
        }
    }

}
