using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // ���� �̹���
    public float fadeDuration = 1f; // ���̵� �ð� (�⺻�� 1��)

    private void Start()
    {
        // ������ �� ���̵� �ƿ� ����
        StartCoroutine(FadeOut());
    }

    public void FadeToNextScene(string sceneName)
    {
        // ���� ������ ���̵� �� ����
        StartCoroutine(FadeInAndLoadScene(sceneName));
    }

    public IEnumerator FadeInAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FadeIn());

        // ���� ������ ��ȯ
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        float time = 0f;
        Color alpha = fadeImage.color;
        alpha.a = 0f; // �ʱ� ���İ� ����
        fadeImage.color = alpha;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeDuration;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadeImage.color = alpha;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float time = 0f;
        Color alpha = fadeImage.color;
        alpha.a = 1f; // �ʱ� ���İ� ����
        fadeImage.color = alpha;

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeDuration;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadeImage.color = alpha;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // ���̵� �ƿ� �� �̹��� ��Ȱ��ȭ
    }
}
