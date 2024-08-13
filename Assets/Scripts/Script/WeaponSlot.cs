using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Image icon;
    public bool IsOccupied { get; private set; }

    public void SetSlot(Sprite newIcon)
    {
        icon.sprite = newIcon;
        icon.enabled = true;
        IsOccupied = true;
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        IsOccupied = false;
    }
}
