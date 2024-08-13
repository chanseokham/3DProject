using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Ŀ�� ũ��
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale; // ���� ũ�� ����
    }

    // ���콺�� ��ư ���� �ö��� �� ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale; // ũ�� ����
    }

    // ���콺�� ��ư���� �������� �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // ���� ũ��� ����
    }
}
