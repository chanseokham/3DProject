using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager; // 스폰 매니저 참조
    public PlayerStats playerStats; // 플레이어 스탯 참조
    public int goldReward = 500; // 스테이지 완료 보상
    public float restTime = 30f; // 쉬는 시간

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
        // 플레이어에게 보상 지급
        playerStats.AddGold(goldReward);

        // 쉬는 시간
        yield return new WaitForSeconds(restTime);

        // 다음 스테이지로 이동
        currentStage++;
        if (currentStage < spawnManager.stageInfos.Length)
        {
            StartStage();
        }
        else
        {
            Debug.Log("모든 스테이지를 완료했습니다!");
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
            yield return new WaitForSeconds(1f); // 1초마다 체크
        }
    }

    bool AllMonstersDefeated()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        return enemyCount == 0;
    }
}
