using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfo", menuName = "Stage/StageInfo", order = 1)]
public class StageInfo : ScriptableObject
{
    public GameObject[] monsters; // �� ������������ ������ ���͵�
    public int[] monsterCounts; // �� ������������ ������ ���͵��� ��
}
