using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAni : MonoBehaviour
{
    private Animator animator;
    private EnemyManager enemyManager; // EnemyManager�� ���� ����(State)�� Ȯ��

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyManager = GetComponent<EnemyManager>();
    }

    void Update()
    {
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        EnemyManager.State currentState = enemyManager.currentState;

        switch (currentState)
        {
            case EnemyManager.State.Idle:
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDead", false);
                break;
            case EnemyManager.State.Chase:
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDead", false);
                break;
            case EnemyManager.State.Attack:
                animator.SetBool("isAttacking", true);
                animator.SetBool("isDead", false);
                break;
            case EnemyManager.State.Dead:
                animator.SetBool("isDead", true);
                break;
        }
    }
}
