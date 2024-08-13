using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BaseStats
{
    public int curGold { get; set; }
    public override void InitStat() //BaseStats�� �ִ� Init���� ������
    {
        szName = "Player";
        statMaxHP = 100;
        statCurHP = statMaxHP;
        curGold = 5000;

        isDead = false;
        UIManager.s.UpdatePlayer(this);
    }

    protected override void UpdateAfterReceiveAttack() //// ü���� 0�� �Ǹ� �״� �޼��� ���
    {
        base.UpdateAfterReceiveAttack();
        Debug.Log(szName + "HP = " + statCurHP.ToString());
        UIManager.s.UpdatePlayer(this);
    }

    public void AddGold(int gold)
    {
        this.curGold += gold;
        UIManager.s.UpdatePlayer(this);
    }

    public bool SpendGold(int price) // �޼����� �Ű����� �̸��� amount���� price�� ����
    {
        if (curGold >= price) // amount�� price�� ����
        {
            curGold -= price; // amount�� price�� ����
            UIManager.s.UpdatePlayer(this);
            return true;
        }
        else
        {
            Debug.Log("Not enough gold.");
            return false;
        }
    }
}
