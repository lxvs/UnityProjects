using UnityEngine;

public class ItemPickup : Interactable
{
    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    private void Pickup()
    {
        Debug.Log("Picked up " + name);
        Destroy(gameObject);
    }
}
