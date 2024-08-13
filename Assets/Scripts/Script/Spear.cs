using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� ������ Ȯ��
        if (other.CompareTag("Enemy"))
        {
            // ���� EnemyManager�� ������
            EnemyManager enemyManager = other.GetComponent<EnemyManager>();

            // ������ ���ظ� ����
            if (enemyManager != null)
            {
                enemyManager.TakeDamage(15); // �÷��̾��� ���ݷ��� 5�� ����
            }
        }
    }
}
