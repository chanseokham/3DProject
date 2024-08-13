using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseStats : MonoBehaviour
{
    public string szName = ""; //플레이어 이름
    public int statMaxHP { get; set; } //최대체력
    public int statCurHP { get; set; } //현재체력
    public bool isDead { get; set; } // 죽었은지 여부
    public int enemyDamage {  get; set; } //몬스터한테 받는 데미지

    [System.NonSerialized]
    public UnityEvent deadEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        InitStat();
    }

    public virtual void InitStat() //BaseStats를 상속한 파생 클래스에서 재정의
    {

    }
    
    public void SetEnemyAttack(int enemyDamage) //몬스터한테 받는 데미지
    {
        statCurHP -= enemyDamage;
        UpdateAfterReceiveAttack(); 
    }

    protected virtual void UpdateAfterReceiveAttack() // 체력이 0이 되면 죽는 메서드 사용
    {
        if (statCurHP <= 0)
        {
            statCurHP = 0;
            isDead = true;

            deadEvent.Invoke();
        }
    }
}
