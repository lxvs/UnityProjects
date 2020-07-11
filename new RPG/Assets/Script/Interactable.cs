using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    public bool isFocused = false;
    public bool hasInteracted = false;
    PlayerController player;

    private void Start()
    {
        
    }
    public void OnFoucused(PlayerController playerController)
    {
        hasInteracted = false;
        isFocused = true;
        player = playerController;
    }

    public void OnDefoucused()
    {
        hasInteracted = false;
        isFocused = false;
        player = null;
    }

    public virtual void Interact()
    {
        Debug.Log("INTERACTED with " + transform.name);
    }

    private void Update()
    {
        if (isFocused && !hasInteracted)
        {
            if (interactionTransform == null) interactionTransform = transform;
            float distant = Vector3.Distance(player.transform.position, interactionTransform.position);
            if (distant <= radius)
            {
                hasInteracted = true;
                Interact();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null) interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    private void OnDestroy()
    {
        Debug.Log(name + " on destroy");
        if (player != null && player.focus != null)
        {
            Debug.Log(name + " was destroyed because it was picked up");
            player.RemoveFocus();
        }
    }
}
