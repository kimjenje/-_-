using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // 검정 이미지
    public float fadeDuration = 1f; // 페이드 시간 (기본값 1초)

    private void Start()
    {
        // 시작할 때 페이드 아웃 실행
        StartCoroutine(FadeOut());
    }

    public void FadeToNextScene(string sceneName)
    {
        // 다음 씬으로 페이드 인 실행
        StartCoroutine(FadeInAndLoadScene(sceneName));
    }

    public IEnumerator FadeInAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FadeIn());

        // 다음 씬으로 전환
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        float time = 0f;
        Color alpha = fadeImage.color;
        alpha.a = 0f; // 초기 알파값 설정
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
        alpha.a = 1f; // 초기 알파값 설정
        fadeImage.color = alpha;

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeDuration;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadeImage.color = alpha;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // 페이드 아웃 후 이미지 비활성화
    }
}
