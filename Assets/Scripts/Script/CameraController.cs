using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Vector3 offset; // ī�޶�� �÷��̾� ������ �Ÿ�
    public float sensitivity = 0.1f; // ���콺 ����

    private float pitch = 0f; // ���� ȸ�� ����
    private float yaw = 0f; // �¿� ȸ�� ����

    void Start()
    {
        // �ʱ� ������ ����
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // ���콺 �Է� �ޱ�
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -35, 60); // ���� ȸ�� ����

        // �÷��̾ �߽����� ī�޶� ȸ��
        transform.position = player.position + offset;
        transform.RotateAround(player.position, Vector3.up, yaw);
        transform.RotateAround(player.position, transform.right, pitch);

        // ī�޶� �׻� �÷��̾ �ٶ󺸵��� ����
        transform.LookAt(player.position + Vector3.up * 1.5f); // �ణ ������ �ٶ󺸰� ����
    }
}
