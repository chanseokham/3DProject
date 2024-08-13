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
    public Sprite[] itemIcons;  // 아이템 아이콘 배열
    public PlayerManager.WeaponType[] itemTypes;  // 아이템 타입 배열
    public WeaponSlotManager weaponSlotManager;  // WeaponSlotManager 참조

    // 플레이어가 상점에 들어갈 때 호출
    public void Enter(PlayerManager player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // 플레이어가 상점에서 나갈 때 호출
    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    // 아이템 구매 함수
    public void Buy(int index)
    {
        // Null 및 유효성 검사
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
