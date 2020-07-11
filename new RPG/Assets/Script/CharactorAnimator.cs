﻿using UnityEngine;
using UnityEngine.AI;

public class CharactorAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;
    Animator animator;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }
}
