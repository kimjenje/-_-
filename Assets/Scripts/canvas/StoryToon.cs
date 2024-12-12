using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryToon : MonoBehaviour
{
    public GameObject[] comicPrefabs; // 만화 프리팹을 담을 배열
    public Transform parentTransform; // 프리팹을 배치할 부모 트랜스폼
    public float interval = 3.0f; // 자동으로 넘어가는 간격

    private int currentIndex = 0; // 현재 활성화된 프리팹 인덱스
    private bool isCoroutineRunning = false; // 코루틴 실행 여부 확인
    public FadeController fadeController;
    public string SceneName; // 다음 씬 이름

    public Button skipButton; // 스킵 버튼

    void Start()
    {
        // 모든 프리팹을 처음에 비활성화
        foreach (GameObject prefab in comicPrefabs)
        {
            prefab.SetActive(false);
        }
        // 첫 번째 프리팹 활성화
        if (comicPrefabs.Length > 0)
        {
            ShowNextPrefab();
        }

        // 스킵 버튼에 메서드 연결
        skipButton.onClick.AddListener(SkipToEnd);
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 기존 코루틴 정지
            if (isCoroutineRunning)
            {
                StopCoroutine("AutoShowNextPrefab");
                isCoroutineRunning = false;
            }
            ShowNextPrefab();
        }
    }

    void ShowNextPrefab()
    {
        if (currentIndex < comicPrefabs.Length)
        {
            if (currentIndex > 0)
            {
                // 이전 프리팹 비활성화
                Destroy(comicPrefabs[currentIndex - 1]);
            }
            // 현재 프리팹 활성화
            GameObject instance = Instantiate(comicPrefabs[currentIndex], parentTransform);
            instance.SetActive(true);
            currentIndex++;

            // 코루틴 시작
            StartCoroutine("AutoShowNextPrefab");
        }
        else
        {
            StartCoroutine(FadeOutAndLoadNextScene());
        }
    }

    IEnumerator AutoShowNextPrefab()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(interval);
        isCoroutineRunning = false;
        ShowNextPrefab();
    }

    IEnumerator FadeOutAndLoadNextScene()
    {
        // 화면 전환 효과를 없애고 바로 씬 전환
        SceneManager.LoadScene(SceneName);
        yield break; // 코루틴 종료
    }

    // 스킵 버튼이 눌렸을 때 호출되는 메서드
    public void SkipToEnd()
    {
        // 코루틴 중지
        StopAllCoroutines();

        // 스킵 버튼 파괴
        Destroy(skipButton.gameObject);

        // 남은 모든 만화를 건너뛰고 씬을 바로 전환
        StartCoroutine(FadeOutAndLoadNextScene());
    }
}
