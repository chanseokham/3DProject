using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerManager enterPlayer;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Sprite[] itemIcons;  // ������ ������ �迭
    public PlayerManager.WeaponType[] itemTypes;  // ������ Ÿ�� �迭
    public WeaponSlotManager weaponSlotManager;  // WeaponSlotManager ����

    // �÷��̾ ������ �� �� ȣ��
    public void Enter(PlayerManager player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // �÷��̾ �������� ���� �� ȣ��
    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    // ������ ���� �Լ�
    public void Buy(int index)
    {
        // Null �� ��ȿ�� �˻�
        if (enterPlayer == null)
        {
            Debug.LogError("enterPlayer is null");
            return;
        }

        if (enterPlayer.playerStats == null)
        {
            Debug.LogError("playerStats is null");
            return;
        }

        if (weaponSlotManager == null)
        {
            Debug.LogError("weaponSlotManager is null");
            return;
        }

        if (index < 0 || index >= itemPrice.Length)
        {
            Debug.LogError("Invalid index for itemPrice");
            return;
        }

        if (index < 0 || index >= itemIcons.Length)
        {
            Debug.LogError("Invalid index for itemIcons");
            return;
        }

        if (index < 0 || index >= itemTypes.Length)
        {
            Debug.LogError("Invalid index for itemTypes");
            return;
        }

        if (itemIcons[index] == null)
        {
            Debug.LogError("itemIcon at index is null");
            return;
        }

        if (enterPlayer.playerStats.curGold >= itemPrice[index])
        {
            enterPlayer.playerStats.AddGold(-itemPrice[index]);
            weaponSlotManager.AddWeapon(itemIcons[index], itemTypes[index]);
        }
    }
}   
