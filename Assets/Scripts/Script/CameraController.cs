using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset; // 카메라와 플레이어 사이의 거리
    public float sensitivity = 0.1f; // 마우스 감도

    private float pitch = 0f; // 상하 회전 각도
    private float yaw = 0f; // 좌우 회전 각도

    void Start()
    {
        // 초기 오프셋 설정
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 마우스 입력 받기
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -35, 60); // 상하 회전 제한

        // 플레이어를 중심으로 카메라 회전
        transform.position = player.position + offset;
        transform.RotateAround(player.position, Vector3.up, yaw);
        transform.RotateAround(player.position, transform.right, pitch);

        // 카메라가 항상 플레이어를 바라보도록 설정
        transform.LookAt(player.position + Vector3.up * 1.5f); // 약간 위쪽을 바라보게 조정
    }
}
