﻿using System.Collections;
using TMPro;
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

    ItemPickup neariestPickup;
    Coroutine pickupDistanceCheckCoroutine;
    void Start()
    {
        cam = Camera.main;
        if (motor == null) motor = GetComponent<PlayerMotor>();
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

        if(Input.GetButtonDown("Operation"))
        {
            if(neariestPickup != null)
            {
                SetFocus(neariestPickup);
                if (pickupDistanceCheckCoroutine != null) StopCoroutine(pickupDistanceCheckCoroutine);
            }
            else if (false)
            {

            }
        }

        pickupDistanceCheckCoroutine =  StartCoroutine("PickupsDistanceCheck");

        Vector3 velocity = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), 1f);
        if (velocity != Vector3.zero) motor.Move(velocity);
        
    }

    IEnumerator PickupsDistanceCheck()
    {
        if (neariestPickup == null || Vector3.Distance(neariestPickup.transform.position, transform.position) > 1f)
        {
            neariestPickup = null;
            float leastDistance = 1f;

            foreach (ItemPickup pickupItem in pickups.GetComponentsInChildren<ItemPickup>())
            {
                if (pickupItem == null) continue;
                float distance = Vector3.Distance(pickupItem.transform.position, transform.position);
                if (distance < (neariestPickup == null ? leastDistance : leastDistance - .3f))  
                {
                    leastDistance = distance;
                    neariestPickup = pickupItem;
                }
                yield return null;
            }

            if (neariestPickup != null)
            {
                UIManager.instance.showPickupHint(neariestPickup.name, "Press " + (GameSettingsManager.instance.isGamePadConnected ? "X button" : "F") + " to get.", Item.itemQualityColor[(int)neariestPickup.item.ItemQuality]);
            }
            else if (UIManager.instance.pickupHint.activeSelf)
            {
                UIManager.instance.showPickupHint("", "", Color.white, true);
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
