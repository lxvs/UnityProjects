using UnityEngine;

[CreateAssetMenu(fileName = "Plastic pad", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "Plastic pad";
    public string discription = "A plastic pad you picked up some day, it's totally useless.";
    public Sprite icon = null;
    public short maxStack = 1000;
    public short maxCarry = 32000;
    public bool pickable = true;
    public short amount = 0;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

}
