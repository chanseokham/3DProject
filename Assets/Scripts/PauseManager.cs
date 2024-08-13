using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // �Ͻ����� �� Ȱ��ȭ�� ������Ʈ
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // ���� �Ͻ�����
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // ���� �簳
        isPaused = false;
    }
}
