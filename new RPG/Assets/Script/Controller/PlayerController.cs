using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    public LayerMask movementMask;
    public PlayerMotor motor;
    public Interactable focus;
    public GameObject pickups;

    bool isUsingMouse = true;
    UIManager uiManager;
    ItemPickup neariestPickup;
    Coroutine pickupDistanceCheckCoroutine;
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        uiManager = UIManager.instance;
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isUsingMouse)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(ray, out raycastHit, 100f, movementMask))
                    {
                        if (focus != null) RemoveFocus();
                        motor.MoveToPoint(raycastHit.point);
                    }
                }
                else
                {
                    if (Physics.Raycast(ray, out raycastHit, 100f))
                    {
                        Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
                        if (interactable != null)
                        {
                            Enemy enemy = interactable as Enemy;
                            ItemPickup itemPickup = interactable as ItemPickup;
                            if (itemPickup != null)
                            {
                                if (focus == null)
                                {
                                    uiManager.ShowPickupHint(itemPickup, isUsingMouse);
                                }
                                if (Input.GetMouseButtonDown(1))
                                {
                                    uiManager.ShowInteractableFocusHint(itemPickup);
                                    SetFocus(itemPickup);
                                }
                            }
                            else if (enemy != null)
                            {
                                if (focus == null)
                                {
                                    uiManager.ShowPickupHint(enemy);
                                }
                                if (Input.GetMouseButtonDown(1))
                                {
                                    uiManager.ShowInteractableFocusHint(enemy);
                                    SetFocus(enemy);
                                }
                            }
                            else
                            {
                                if (focus == null)
                                {
                                    uiManager.ShowPickupHint(interactable.name);
                                }
                                if (Input.GetMouseButtonDown(1))
                                {
                                    uiManager.ShowInteractableFocusHint(interactable.name);
                                    SetFocus(interactable);
                                }
                            }
                        }
                        else if (focus == null)
                        {
                            uiManager.HidePickupHint();
                        }

                    }
                    else if (focus == null)
                    {
                        uiManager.HidePickupHint();
                    }

                }
            }
            else
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                {
                    isUsingMouse = true;
                }
            }
        }
        
        if (!isUsingMouse)
        {
            if (Input.GetButtonDown("Operation"))
            {
                if (neariestPickup != null)
                {
                    SetFocus(neariestPickup);
                    if (pickupDistanceCheckCoroutine != null) StopCoroutine(pickupDistanceCheckCoroutine);
                }
            }

            pickupDistanceCheckCoroutine = StartCoroutine("PickupsDistanceCheck");

            Vector3 velocity = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);
            if (velocity != Vector3.zero)
            {
                motor.Move(velocity);
            }
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isUsingMouse = false;
        }
    }

    IEnumerator PickupsDistanceCheck()
    {
        if (neariestPickup == null || Vector3.Distance(neariestPickup.transform.position, transform.position) > 2f)
        {
            neariestPickup = null;
            float leastDistance = 2f;

            foreach (ItemPickup pickupItem in pickups.GetComponentsInChildren<ItemPickup>())
            {
                if (pickupItem == null) continue;
                float distance = Vector3.Distance(pickupItem.transform.position, transform.position);
                if (distance < (neariestPickup == null ? leastDistance : leastDistance - .5f))  
                {
                    leastDistance = distance;
                    neariestPickup = pickupItem;
                }
                yield return null;
            }

            if (neariestPickup != null)
            {
                uiManager.ShowPickupHint(neariestPickup, isUsingMouse);
            }
            else if (uiManager.promptBar.activeSelf)
            {
                uiManager.HidePickupHint();
            }
        }

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
