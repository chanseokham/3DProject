using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseStats : MonoBehaviour
{
    public string szName = ""; //�÷��̾� �̸�
    public int statMaxHP { get; set; } //�ִ�ü��
    public int statCurHP { get; set; } //����ü��
    public bool isDead { get; set; } // �׾����� ����
    public int enemyDamage {  get; set; } //�������� �޴� ������

    [System.NonSerialized]
    public UnityEvent deadEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        InitStat();
    }

    public virtual void InitStat() //BaseStats�� ����� �Ļ� Ŭ�������� ������
    {

    }
    
    public void SetEnemyAttack(int enemyDamage) //�������� �޴� ������
    {
        statCurHP -= enemyDamage;
        UpdateAfterReceiveAttack(); 
    }

    protected virtual void UpdateAfterReceiveAttack() // ü���� 0�� �Ǹ� �״� �޼��� ���
    {
        if (statCurHP <= 0)
        {
            statCurHP = 0;
            isDead = true;

            deadEvent.Invoke();
        }
    }
}
