using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    public LayerMask movementMask;
    public PlayerMotor motor;
    public Interactable focus;
    
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, 100f, movementMask))
                {
                    if (focus != null) RemoveFocus();
                    motor.MoveToPoint(raycastHit.point);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, 100f))
                {
                    Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                    else
                    {
                        RemoveFocus();
                    }
                }
            }
        }

        Vector3 velocity = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);
        if (velocity != Vector3.zero) motor.Move(velocity);
        
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null) focus.OnDefoucused();
            focus = newFocus;
            focus.OnFoucused(this);
            motor.FollowTarget(newFocus);
        }
        else if (newFocus.hasInteracted)
        {
            newFocus.hasInteracted = false;
        }
    }

    public void RemoveFocus()
    {
        if (focus != null) 
        {
            focus.OnDefoucused();
            focus = null;
            motor.StopFollowingTarget();
        }
        
    }
}
