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


    /// <summary>
    /// Show a hint for a pickup item, with a half black bg and white caption.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="itemQuality"></param>
    /// <param name="caption"></param>
    public void ShowPickupHint(ItemPickup itemPickup, bool isUsingMouse, string caption = null)
    {
        pickupHintTitle.text = itemPickup.item.name;
        pickupHintTitle.color = Item.itemQualityColor[(int)itemPickup.item.ItemQuality];
        if (isUsingMouse)
        {
            pickupHintCaption.text = caption ?? "Click it to pick it up.";

        }
        else
        {
            pickupHintCaption.text = caption ?? "Press " + (GameSettingsManager.instance.isGamePadConnected ? "X button" : "F") + " to pick it up.";

        }
        pickupHintCaption.color = Color.white;
        pickupHintBg.color = new Color(0, 0, 0, .5f);
        if (!pickupHint.activeSelf) pickupHint.SetActive(true);
    }

    public void ShowPickupHint(Enemy enemy, Color? bgColor = null)
    {

        pickupHintTitle.text = enemy.name;
        pickupHintTitle.color = Enemy.enemyQualityColor[(int)enemy.enemyQuality];
        pickupHintCaption.text = "Lv." + enemy.enemyStats.lvl + "  " + enemy.enemyStats.hp + "/" + enemy.enemyStats.hpm.GetValue();
        pickupHintCaption.color = Color.white;
        pickupHintBg.color = bgColor ?? new Color(0, 0, 0, .5f);
        if (!pickupHint.activeSelf) pickupHint.SetActive(true);

    }

    public void ShowPickupHint(string title, string Caption = null, Color? titleColor = null, Color? bgColor = null, Color? captionColor = null)
    {

        pickupHintTitle.text = title;
        pickupHintTitle.color = titleColor ?? Color.white;
        pickupHintCaption.text = Caption;
        pickupHintCaption.color = captionColor ?? Color.white;
        pickupHintBg.color = bgColor ?? new Color(0, 0, 0, .5f);
        if (!pickupHint.activeSelf) pickupHint.SetActive(true);
        
    }

    public void ShowInteractableFocusHint(ItemPickup itemPickup, string caption = null, Color? bgColor = null)
    {
        pickupHintTitle.text = itemPickup.item.name;
        pickupHintTitle.color = Item.itemQualityColor[(int)itemPickup.item.ItemQuality];
        pickupHintCaption.text = caption;
        pickupHintBg.color = bgColor ?? new Color(0, 0, .5f, .5f);
        if (!pickupHint.activeSelf) pickupHint.SetActive(true);
    }

    public void ShowInteractableFocusHint(Enemy enemy, Color? bgColor = null)
    {
        pickupHintTitle.text = enemy.name;
        pickupHintTitle.color = Enemy.enemyQualityColor[(int)enemy.enemyQuality];
        pickupHintCaption.text = "Lv." + enemy.enemyStats.lvl + "  " + enemy.enemyStats.hp + "/" + enemy.enemyStats.hpm.GetValue();
        pickupHintBg.color = bgColor ?? new Color(.5f, 0, 0, .5f);
        if (!pickupHint.activeSelf) pickupHint.SetActive(true);
    }

    public void ShowInteractableFocusHint(string title, string Caption = null, Color? titleColor = null, Color? bgColor = null, Color? captionColor = null)
    {

        pickupHintTitle.text = title;
        pickupHintTitle.color = titleColor ?? Color.white;
        pickupHintCaption.text = Caption;
        pickupHintCaption.color = captionColor ?? Color.white;
        pickupHintBg.color = bgColor ?? new Color(0, .5f, .5f, .5f);
        if (!pickupHint.activeSelf) pickupHint.SetActive(true);

    }
}
