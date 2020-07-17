using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1f;
    public Transform interactionTransform;

    public bool isFocused = false;
    public bool hasInteracted = false;
    PlayerController player;

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
        if (player != null && player.focus != null)
        {
            player.RemoveFocus();
        }
    }
}
