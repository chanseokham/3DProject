    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint; // ȭ���� ������ ��ġ
    public int arrowDamage = 10;
    public float arrowSpeed = 20f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ShootArrow(Vector3 direction, float speed)
    {
        if (arrowSpawnPoint != null)
        {
            // ȭ�� �������� ArrowSpawnPoint�� ȸ���� ��ġ�ϰ� ����
            GameObject arrowObject = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            Arrow arrow = arrowObject.GetComponent<Arrow>();

            // ȭ���� X���� 90�� ȸ����ŵ�ϴ�.
            arrowObject.transform.Rotate(-90f, 0f, 90f);

            if (arrow != null)
            {
                arrow.damage = arrowDamage;
                arrow.Shoot(direction, speed); // ȭ�� �߻�
            }
        }
        else
        {
            Debug.LogError("ArrowSpawnPoint�� �������� �ʾҽ��ϴ�.");
        }
    }
}
