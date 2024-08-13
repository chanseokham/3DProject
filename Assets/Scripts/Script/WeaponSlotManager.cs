using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotManager : MonoBehaviour
{
    public Image[] weaponSlotImages;  // Weapon slots UI �̹��� �迭
    public Sprite defaultSwordIcon;   // �⺻ �� ������

    private Dictionary<Sprite, PlayerManager.WeaponType> weaponIconToTypeMap;

    void Start()
    {
        weaponIconToTypeMap = new Dictionary<Sprite, PlayerManager.WeaponType>
        {
            { defaultSwordIcon, PlayerManager.WeaponType.Sword }
            // �ٸ� ���� �����ܰ� Ÿ���� ���⿡ �߰�
        };

        // �⺻ ���⸦ ù ��° ���Կ� ����
        if (weaponSlotImages.Length > 0 && defaultSwordIcon != null)
        {
            weaponSlotImages[0].sprite = defaultSwordIcon;
            weaponSlotImages[0].enabled = true;  // �̹��� Ȱ��ȭ
        }
        else
        {
            Debug.LogError("WeaponSlotManager: �⺻ �� ������ �Ǵ� ���� �̹����� �������� �ʾҽ��ϴ�.");
        }
    }

    // ���� �߰� �Լ�
    public void AddWeapon(Sprite weaponIcon, PlayerManager.WeaponType weaponType)
    {
        weaponIconToTypeMap[weaponIcon] = weaponType; // ���� Ÿ���� ����

        for (int i = 1; i < weaponSlotImages.Length; i++)  // ù ��° ������ �⺻ ������ ����
        {
            if (weaponSlotImages[i].sprite == null)
            {
                weaponSlotImages[i].sprite = weaponIcon;
                weaponSlotImages[i].enabled = true;  // �̹��� Ȱ��ȭ
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