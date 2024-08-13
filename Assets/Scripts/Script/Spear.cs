using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 적인지 확인
        if (other.CompareTag("Enemy"))
        {
            // 적의 EnemyManager를 가져옴
            EnemyManager enemyManager = other.GetComponent<EnemyManager>();

            // 적에게 피해를 입힘
            if (enemyManager != null)
            {
                enemyManager.TakeDamage(15); // 플레이어의 공격력을 5로 가정
            }
        }
    }
}
