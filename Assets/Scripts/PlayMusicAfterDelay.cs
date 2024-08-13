using UnityEngine;
using System.Collections;

public class PlayMusicAfterDelay : MonoBehaviour
{
    public AudioSource audioSource; // ����� �ҽ� ������Ʈ�� �����մϴ�.
    public float delay = 3f; // ������ �ð� (3��)

    void Start()
    {
        StartCoroutine(PlayMusicAfterDelayCoroutine());
    }

    IEnumerator PlayMusicAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
