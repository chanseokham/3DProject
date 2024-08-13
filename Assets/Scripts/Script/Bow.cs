    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint; // 화살이 생성될 위치
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
            // 화살 프리팹을 ArrowSpawnPoint의 회전과 일치하게 생성
            GameObject arrowObject = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            Arrow arrow = arrowObject.GetComponent<Arrow>();

            // 화살의 X축을 90도 회전시킵니다.
            arrowObject.transform.Rotate(-90f, 0f, 90f);

            if (arrow != null)
            {
                arrow.damage = arrowDamage;
                arrow.Shoot(direction, speed); // 화살 발사
            }
        }
        else
        {
            Debug.LogError("ArrowSpawnPoint가 설정되지 않았습니다.");
        }
    }
}
