using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager; // ���� �Ŵ��� ����
    public PlayerStats playerStats; // �÷��̾� ���� ����
    public int goldReward = 500; // �������� �Ϸ� ����
    public float restTime = 30f; // ���� �ð�

    private int currentStage = 0;

    void Start()
    {
        StartStage();
    }

    void StartStage()
    {
        spawnManager.StartStage(currentStage);
        StartCoroutine(CheckStageCompletion());
    }

    public void EndStage()
    {
        StartCoroutine(RestAndReward());
    }

    IEnumerator RestAndReward()
    {
        // �÷��̾�� ���� ����
        playerStats.AddGold(goldReward);

        // ���� �ð�
        yield return new WaitForSeconds(restTime);

        // ���� ���������� �̵�
        currentStage++;
        if (currentStage < spawnManager.stageInfos.Length)
        {
            StartStage();
        }
        else
        {
            Debug.Log("��� ���������� �Ϸ��߽��ϴ�!");
        }
    }

    IEnumerator CheckStageCompletion()
    {
        while (true)
        {
            if (AllMonstersDefeated())
            {
                EndStage();
                break;
            }
            yield return new WaitForSeconds(1f); // 1�ʸ��� üũ
        }
    }

    bool AllMonstersDefeated()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        return enemyCount == 0;
    }
}
