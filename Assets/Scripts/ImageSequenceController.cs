using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSequenceController : MonoBehaviour
{
    public Image firstImage;
    public Image secondImage;
    public RawImage thirdImage; // ����� �κ�
    public Image fourthImage;
    public Image fifthImage;
    public float fadeDuration = 2f;
    public float finalFadeDuration = 1f; // ������ �ܰ��� ���� �ð�

    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // ù ��° �̹����� ������ 0���� 255�� ����
        yield return StartCoroutine(FadeImage(firstImage, 0f, 1f, fadeDuration));

        // ù ��° �̹����� ������ 255���� 0���� ����
        yield return StartCoroutine(FadeImage(firstImage, 1f, 0f, fadeDuration));

        // �� ��° �̹����� ������ 0���� 255�� ������Ű�� �������� 0���� 1�� ����, ���ÿ� �� ��° �̹����� ������ 0���� 255�� ����
        yield return StartCoroutine(FadeInAndScaleUp(secondImage, thirdImage));

        // �� ��° �̹����� �ټ� ��° �̹����� ������ ���ÿ� 0���� 255�� 1�� �ȿ� ����
        yield return StartCoroutine(FadeImagesSimultaneously(fourthImage, fifthImage, 0f, 1f, finalFadeDuration));
    }

    IEnumerator FadeImage(Graphic image, float startAlpha, float endAlpha, float duration) // Image���� Graphic���� �����Ͽ� Image�� RawImage ��� ����
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, endAlpha);
    }

    IEnumerator FadeInAndScaleUp(Image fadeInImage, RawImage fadeInScaleImage) // ����� �κ�
    {
        float elapsedTime = 0f;
        Color fadeInColor = fadeInImage.color;
        Color scaleColor = fadeInScaleImage.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeInImage.color = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, alpha);
            fadeInScaleImage.color = new Color(scaleColor.r, scaleColor.g, scaleColor.b, alpha);

            float scale = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeInImage.transform.localScale = new Vector3(scale, scale, 1f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeInImage.color = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 1f);
        fadeInScaleImage.color = new Color(scaleColor.r, scaleColor.g, scaleColor.b, 1f);
        fadeInImage.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    IEnumerator FadeImagesSimultaneously(Graphic image1, Graphic image2, float startAlpha, float endAlpha, float duration) // Image���� Graphic���� �����Ͽ� Image�� RawImage ��� ����
    {
        float elapsedTime = 0f;
        Color color1 = image1.color;
        Color color2 = image2.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image1.color = new Color(color1.r, color1.g, color1.b, alpha);
            image2.color = new Color(color2.r, color2.g, color2.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image1.color = new Color(color1.r, color1.g, color1.b, endAlpha);
        image2.color = new Color(color2.r, color2.g, color2.b, endAlpha);
    }
}
