using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public StageInfo[] stageInfos; // �� ���������� ���� ����
    public GameObject[] spawnPortals; // ���� ��ġ ������Ʈ �迭

    public void StartStage(int stageIndex)
    {
        StageInfo stageInfo = stageInfos[stageIndex];

        for (int i = 0; i < stageInfo.monsters.Length; i++)
        {
            GameObject monsterPrefab = stageInfo.monsters[i];
            int monsterCount = stageInfo.monsterCounts[i];

            for (int j = 0; j < monsterCount; j++)
            {
                // ���͸� �����մϴ�.
                SpawnMonster(monsterPrefab, spawnPortals[Random.Range(0, spawnPortals.Length)].transform.position);
            }
        }
    }

    void SpawnMonster(GameObject monsterPrefab, Vector3 spawnPosition)
    {
        // ���͸� �����մϴ�.
        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}
