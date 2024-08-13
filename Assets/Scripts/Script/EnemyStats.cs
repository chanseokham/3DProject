using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyStats : BaseStats
{
    public Image imageHP;
    // Start is called before the first frame update
    public override void InitStat()
    {
        /*
        szName = "Monster";
        statLevel = 1;
        statMaxHP = 50;
        statCurHP = statMaxHP;
        statMaxPW = 5;
        statMinPW = 2;
        statDP = 1;

        plusExp = 10;
        plusGold = Random.Range(10, 30);
        */
        XMLManager.s.LoadMonsterParamsFromXML(szName, this);

        isDead = false;

        //1�ܰ� - HP �ʱ�ȭ
        InitHP();
    }

    //1�ܰ� - HP �ʱ�ȭ
    void InitHP()
    {
        imageHP.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();

        Debug.Log(szName + " HP = " + statCurHP.ToString());
        imageHP.rectTransform.localScale = new Vector3((float)statCurHP / (float)statMaxHP, 1f, 1f);
    }
}
