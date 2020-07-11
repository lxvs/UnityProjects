using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    private void Pickup()
    {
        short itemAddStatus = Inventory.instance.Add(item);
        switch (itemAddStatus)
        {
            case 0:
                Destroy(gameObject);
                Debug.Log("Picked up " + item.name);
                break;
            case 1:
                Debug.LogWarning(item.name + " is not pickable!");
                break;
            case 2:
                Debug.LogWarning(item.name + " 2222222");
                break;
            default:
                break;
                
        }
    }
}
