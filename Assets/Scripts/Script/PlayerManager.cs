using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 1.0f; // �̵� �ӵ�
    public float rotateSpeed = 150.0f; // ȸ���ӵ� 
    public PlayerState currentState = PlayerState.Idle; //�⺻ ���°� Idle

    private PlayerAni myAni;
    public PlayerStats playerStats;

    public float attackDelay = 2.0f; //���� �ð� ����
    private float attackTimer = 0.0f; //���� üũ�ð�

    public enum WeaponType { None, Sword, Bow, Spear }
    public WeaponType currentWeapon = WeaponType.Sword;
    public WeaponSlotManager weaponSlotManager;

    public GameObject sword;  // Sword ������Ʈ
    public GameObject bow;    // Bow ������Ʈ
    public GameObject spear;  // Spear ������Ʈ

    GameObject nearObject;
    private Bow bowScript;

    // Start is called before the first frame update
    void Start()
    {
        myAni = GetComponent<PlayerAni>();
        playerStats = GetComponent<PlayerStats>();

        // WeaponSlotManager �Ҵ� Ȯ�� �� �ڵ� �Ҵ�
        if (weaponSlotManager == null)
        {
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        if (weaponSlotManager == null)
        {
            Debug.LogError("WeaponSlotManager is not assigned in PlayerManager");
        }
        else
        {
            Debug.Log("WeaponSlotManager is assigned in PlayerManager");
        }

        ChangeState(PlayerState.Idle);

        // �⺻ ���� ����
        if (weaponSlotManager != null && weaponSlotManager.weaponSlotImages.Length > 0)
        {
            weaponSlotManager.weaponSlotImages[0].sprite = weaponSlotManager.defaultSwordIcon;
            weaponSlotManager.weaponSlotImages[0].enabled = true;
        }

        // �ʱ� ���� ���� (�⺻ ���� Ȱ��ȭ)
        ActivateWeapon(WeaponType.Sword);

        // Bow ��ũ��Ʈ ����
        bowScript = bow.GetComponent<Bow>();
    }

    public void CurrentEnemyDead()
    {
        ChangeState(PlayerState.Idle);
    }

    public void ChangeToPlayerDead()
    {
        ChangeState(PlayerState.Die);
    }

    void ChangeState(PlayerState newState)
    {
        if (currentState == newState)
            return;

        myAni.ChangeAni(newState);
        currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        HandleMovement();
        Attack();
        Interact();
        HandleWeaponChange();
    }

    void Interact()
    {
        if (nearObject != null && Input.GetKeyDown(KeyCode.E))
        {
            if (nearObject.CompareTag("Shop"))
            {
                Shop shop = nearObject.GetComponent<Shop>();
                shop.Enter(this);
            }
        }
    }

    void UpdateState() //�÷��̾� ���� ������Ʈ
    {
        switch (currentState)
        {
            case PlayerState.Idle:
                PlayerStateIdle();
                break;
            case PlayerState.Walk:
                PlayerStateWalk();
                break;
            case PlayerState.SwordAttack:
            case PlayerState.BowAttack:
            case PlayerState.SpearAttack:
                PlayerStateAttack();
                break;
            case PlayerState.AttackIdle:
                PlayerStateAttackIdle();
                break;
            case PlayerState.Die:
                PlayerStateDie();
                break;
        }
    }

    // �÷��̾� Idle ���� ����
    void PlayerStateIdle()
    {
        // Idle ���¿� ���� ����
    }

    // �÷��̾� Walk ���� ����
    void PlayerStateWalk()
    {
        // Walk ���¿� ���� ����
        HandleMovement();
    }

    // �÷��̾� Attack ���� ����
    void PlayerStateAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            switch (currentWeapon)
            {
                case WeaponType.Sword:
                    ChangeState(PlayerState.SwordAttack);
                    break;
                case WeaponType.Bow:
                    ChangeState(PlayerState.BowAttack);
                    Vector3 direction = (transform.forward + Vector3.up * 0.1f).normalized; // �ణ�� �� ������ ���� ��Ȯ�� ���ư��� ��
                    bowScript.ShootArrow(direction, bowScript.arrowSpeed); // ȭ�� �߻�
                    break;
                case WeaponType.Spear:
                    ChangeState(PlayerState.SpearAttack);
                    break;
            }
            attackTimer = 0; // ���� �� Ÿ�̸� �ʱ�ȭ
        }
    }

    // �÷��̾� AttackIdle ���� ����
    void PlayerStateAttackIdle()
    {

    }

    // �÷��̾� Die ���� ����
    void PlayerStateDie()
    {
        // Die ���¿� ���� ����
    }

    void HandleMovement() //�̵� ����
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        float verticalInput = Input.GetAxis("Vertical1");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            // ī�޶� �ٶ󺸴� ������ �������� �̵� ������ �����մϴ�.
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0; // Y�� ���� ����
            moveDirection.Normalize(); // ����ȭ

            // �÷��̾ �Է� �������� ȸ����ŵ�ϴ�.
            transform.rotation = Quaternion.LookRotation(moveDirection);
            // �÷��̾ �Է� �������� �̵���ŵ�ϴ�.
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            // Walk ���·� �����մϴ�.
            ChangeState(PlayerState.Walk);
        }
        else
        {
            // �̵� ������ ���ٸ� Idle ���·� �����մϴ�.
            ChangeState(PlayerState.Idle);
        }
    }

    // �÷��̾ ���콺 ��Ŭ������ ������ �� ȣ��
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (currentWeapon)
            {
                case WeaponType.Sword:
                    ChangeState(PlayerState.SwordAttack);
                    break;
                case WeaponType.Bow:
                    ChangeState(PlayerState.BowAttack);
                    Vector3 direction = (transform.forward + Vector3.up * 0.1f).normalized; // �ణ�� �� ������ ���� ��Ȯ�� ���ư��� ��
                    bowScript.ShootArrow(direction, bowScript.arrowSpeed); // ȭ�� �߻�
                    break;
                case WeaponType.Spear:
                    ChangeState(PlayerState.SpearAttack);
                    break;
            }
        }
    }

    // �÷��̾ ������ �� ȣ��Ǵ� �޼���
    public void AttackEnemy(EnemyManager enemyManager)
    {
        // ���� ü���� ���ҽ�Ű�� ���� ���� ������ ����
        int damage = playerStats.enemyDamage;
        enemyManager.TakeDamage(damage);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            nearObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            nearObject = null;
        }
    }

    void HandleWeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);  // �⺻ ��
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);  // �� ��° ����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(2);  // �� ��° ����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(3);  // �� ��° ����
        }
    }

    void ChangeWeapon(int slotIndex)
    {
        if (weaponSlotManager == null)
        {
            Debug.LogError("WeaponSlotManager is not assigned");
            return;
        }

        if (slotIndex >= 0 && slotIndex < weaponSlotManager.weaponSlotImages.Length)
        {
            WeaponType weaponType = weaponSlotManager.GetWeaponTypeBySlotIndex(slotIndex);
            if (weaponType != WeaponType.None)
            {
                currentWeapon = weaponType;
                ActivateWeapon(weaponType);
                Debug.Log("Weapon changed to: " + weaponType);
            }
            else
            {
                Debug.LogError("Weapon type is None for slot index: " + slotIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid slot index: " + slotIndex);
        }
    }

    void ActivateWeapon(WeaponType weaponType)
    {
        // ��� ���� ��Ȱ��ȭ
        sword.SetActive(false);
        bow.SetActive(false);
        spear.SetActive(false);

        // ������ ���� Ȱ��ȭ
        switch (weaponType)
        {
            case WeaponType.Sword:
                sword.SetActive(true);
                break;
            case WeaponType.Bow:
                bow.SetActive(true);
                break;
            case WeaponType.Spear:
                spear.SetActive(true);
                break;
        }
    }
}
