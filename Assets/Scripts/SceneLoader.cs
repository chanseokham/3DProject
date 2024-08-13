using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �� �޼��带 ��ư Ŭ�� �̺�Ʈ�� �����մϴ�.
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ����: Ư�� ���� �ε��ϴ� �޼���
    public void LoadSpecificScene()
    {
        SceneManager.LoadScene("map"); // �ε��Ϸ��� ���� �̸����� ����
    }
}
