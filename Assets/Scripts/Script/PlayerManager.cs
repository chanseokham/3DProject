using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 이동 속도
    public float rotateSpeed = 150.0f; // 회전속도 
    public PlayerState currentState = PlayerState.Idle; //기본 상태값 Idle

    private PlayerAni myAni;
    public PlayerStats playerStats;

    public float attackDelay = 2.0f; //공격 시간 간격
    private float attackTimer = 0.0f; //공격 체크시간

    public enum WeaponType { None, Sword, Bow, Spear }
    public WeaponType currentWeapon = WeaponType.Sword;
    public WeaponSlotManager weaponSlotManager;

    public GameObject sword;  // Sword 오브젝트
    public GameObject bow;    // Bow 오브젝트
    public GameObject spear;  // Spear 오브젝트

    GameObject nearObject;
    private Bow bowScript;

    // Start is called before the first frame update
    void Start()
    {
        myAni = GetComponent<PlayerAni>();
        playerStats = GetComponent<PlayerStats>();

        // WeaponSlotManager 할당 확인 및 자동 할당
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

        // 기본 무기 설정
        if (weaponSlotManager != null && weaponSlotManager.weaponSlotImages.Length > 0)
        {
            weaponSlotManager.weaponSlotImages[0].sprite = weaponSlotManager.defaultSwordIcon;
            weaponSlotManager.weaponSlotImages[0].enabled = true;
        }

        // 초기 무기 설정 (기본 무기 활성화)
        ActivateWeapon(WeaponType.Sword);

        // Bow 스크립트 참조
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

    void UpdateState() //플레이어 상태 업데이트
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

    // 플레이어 Idle 상태 동작
    void PlayerStateIdle()
    {
        // Idle 상태에 대한 동작
    }

    // 플레이어 Walk 상태 동작
    void PlayerStateWalk()
    {
        // Walk 상태에 대한 동작
        HandleMovement();
    }

    // 플레이어 Attack 상태 동작
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
                    Vector3 direction = (transform.forward + Vector3.up * 0.1f).normalized; // 약간의 위 방향을 더해 정확히 날아가게 함
                    bowScript.ShootArrow(direction, bowScript.arrowSpeed); // 화살 발사
                    break;
                case WeaponType.Spear:
                    ChangeState(PlayerState.SpearAttack);
                    break;
            }
            attackTimer = 0; // 공격 후 타이머 초기화
        }
    }

    // 플레이어 AttackIdle 상태 동작
    void PlayerStateAttackIdle()
    {

    }

    // 플레이어 Die 상태 동작
    void PlayerStateDie()
    {
        // Die 상태에 대한 동작
    }

    void HandleMovement() //이동 로직
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        float verticalInput = Input.GetAxis("Vertical1");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            // 카메라가 바라보는 방향을 기준으로 이동 방향을 조정합니다.
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0; // Y축 방향 제거
            moveDirection.Normalize(); // 정규화

            // 플레이어를 입력 방향으로 회전시킵니다.
            transform.rotation = Quaternion.LookRotation(moveDirection);
            // 플레이어를 입력 방향으로 이동시킵니다.
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            // Walk 상태로 변경합니다.
            ChangeState(PlayerState.Walk);
        }
        else
        {
            // 이동 방향이 없다면 Idle 상태로 변경합니다.
            ChangeState(PlayerState.Idle);
        }
    }

    // 플레이어가 마우스 좌클릭으로 공격할 때 호출
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
                    Vector3 direction = (transform.forward + Vector3.up * 0.1f).normalized; // 약간의 위 방향을 더해 정확히 날아가게 함
                    bowScript.ShootArrow(direction, bowScript.arrowSpeed); // 화살 발사
                    break;
                case WeaponType.Spear:
                    ChangeState(PlayerState.SpearAttack);
                    break;
            }
        }
    }

    // 플레이어가 공격할 때 호출되는 메서드
    public void AttackEnemy(EnemyManager enemyManager)
    {
        // 적의 체력을 감소시키는 등의 공격 동작을 수행
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
            ChangeWeapon(0);  // 기본 검
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);  // 두 번째 슬롯
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(2);  // 세 번째 슬롯
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(3);  // 네 번째 슬롯
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
        // 모든 무기 비활성화
        sword.SetActive(false);
        bow.SetActive(false);
        spear.SetActive(false);

        // 선택한 무기 활성화
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
