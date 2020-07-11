﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    Interactable target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        
        StartCoroutine("NavToTarget");
        
    }
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget (Interactable newTarget)
    {
        target = newTarget;
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;       // handle it myself;
    }

    public void StopFollowingTarget ()
    {
        agent.ResetPath();
        target = null;
        agent.stoppingDistance = 0;
        agent.updateRotation = true;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator NavToTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.interactionTransform.position);
            FaceTarget();
            yield return new WaitForSeconds(.1f);
        }
    }
}
