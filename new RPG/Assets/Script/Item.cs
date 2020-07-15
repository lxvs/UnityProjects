using UnityEngine;

public enum ItemQuality { Common, Uncommon, Rare, Epic, Suits, Unique, Special }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public string discription;
    public Sprite icon = null;
    /// <summary>
    /// 0-Common, 1-Uncommon, 2-Rare, 3-Epic, 4-Lagendary, 5-Unique, 6-Special.
    /// </summary>
    public ItemQuality ItemQuality = ItemQuality.Common;
    public short levelLimit = 0;
    public bool pickable = true;
    /// <summary>
    /// tangible items can be put into inventory.
    /// </summary>
    public bool tangible = true;

    public static Color[] itemQualityColor = { Color.white, Color.blue, Color.cyan, Color.yellow, Color.green, Color.magenta, Color.red};

    public virtual void Use()
    {
        Debug.Log("Using <color=" + itemQualityColor[(int)ItemQuality] + ">" + name + "</color>.");
    }

}
