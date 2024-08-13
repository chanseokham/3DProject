using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotManager : MonoBehaviour
{
    public Image[] weaponSlotImages;  // Weapon slots UI 이미지 배열
    public Sprite defaultSwordIcon;   // 기본 검 아이콘

    private Dictionary<Sprite, PlayerManager.WeaponType> weaponIconToTypeMap;

    void Start()
    {
        weaponIconToTypeMap = new Dictionary<Sprite, PlayerManager.WeaponType>
        {
            { defaultSwordIcon, PlayerManager.WeaponType.Sword }
            // 다른 무기 아이콘과 타입을 여기에 추가
        };

        // 기본 무기를 첫 번째 슬롯에 설정
        if (weaponSlotImages.Length > 0 && defaultSwordIcon != null)
        {
            weaponSlotImages[0].sprite = defaultSwordIcon;
            weaponSlotImages[0].enabled = true;  // 이미지 활성화
        }
        else
        {
            Debug.LogError("WeaponSlotManager: 기본 검 아이콘 또는 슬롯 이미지가 설정되지 않았습니다.");
        }
    }

    // 무기 추가 함수
    public void AddWeapon(Sprite weaponIcon, PlayerManager.WeaponType weaponType)
    {
        weaponIconToTypeMap[weaponIcon] = weaponType; // 무기 타입을 매핑

        for (int i = 1; i < weaponSlotImages.Length; i++)  // 첫 번째 슬롯은 기본 검으로 고정
        {
            if (weaponSlotImages[i].sprite == null)
            {
                weaponSlotImages[i].sprite = weaponIcon;
                weaponSlotImages[i].enabled = true;  // 이미지 활성화
                break;
            }
        }
    }

    public PlayerManager.WeaponType GetWeaponTypeBySlotIndex(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < weaponSlotImages.Length)
        {
            Sprite weaponIcon = weaponSlotImages[slotIndex].sprite;
            if (weaponIconToTypeMap.ContainsKey(weaponIcon))
            {
                return weaponIconToTypeMap[weaponIcon];
            }
        }
        return PlayerManager.WeaponType.None;
    }
}