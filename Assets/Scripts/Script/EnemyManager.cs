using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyManager : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Attack,
        Dead
    }

    public State currentState = State.Idle;
    private Animator animator;
    private EnemyStats enemyStats;
    private PlayerStats playerStats;

    private float attackTimer = 0.0f;
    private float attackDelay = 2.0f;

    private float chaseDistance = 1000.0f;
    private float attackDistance = 2.5f;
    private float reChaseDistance = 3.0f;

    private Transform player; // Player�� �ν����Ϳ��� �����ϱ� ���� ����

    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
        enemyStats.deadEvent.AddListener(CallDeadEvent);
        nav = GetComponent<NavMeshAgent>();


        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStats = player.GetComponent<PlayerStats>();

    }
    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Chase:
                ChaseState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Dead:
                DeadState();
                break;
        }
    }

    private void IdleState()
    {
        if (GetDistanceFromPlayer() < chaseDistance)
        {
            ChangeState(State.Chase);
        }
    }

    private void ChaseState()
    {
        nav.SetDestination(player.position);

        if (GetDistanceFromPlayer() < attackDistance)
        {
            ChangeState(State.Attack);
        }
    }

    private void AttackState()
    {
        if (GetDistanceFromPlayer() > reChaseDistance)
        {
            attackTimer = 0;
            ChangeState(State.Chase);
        }
        else
        {
            if (attackTimer > attackDelay)
            {
                transform.LookAt(player.position);
                AttackCalculate();
                attackTimer = 0f;
            }
            attackTimer += Time.deltaTime;
        }
    }

    private void DeadState()
    {
        animator.SetTrigger("Dead"); // ��� �ִϸ��̼� ���
        GetComponent<Collider>().enabled = false; // �ݶ��̴� ��Ȱ��ȭ
        Destroy(gameObject, 2f); // ���� �ð� �� ������Ʈ �ı�
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
    }

    private float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    private void AttackCalculate()
    {
        int damage = enemyStats.enemyDamage;
        playerStats.SetEnemyAttack(damage);
    }

    private void CallDeadEvent()
    {
        ChangeState(State.Dead);
    }

    public void TakeDamage(int damage)
    {
        enemyStats.statCurHP -= damage;
        if (enemyStats.statCurHP <= 0)
        {
            CallDeadEvent();
        }
    }
}
