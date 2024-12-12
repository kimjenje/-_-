using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryToon : MonoBehaviour
{
    public GameObject[] comicPrefabs; // ��ȭ �������� ���� �迭
    public Transform parentTransform; // �������� ��ġ�� �θ� Ʈ������
    public float interval = 3.0f; // �ڵ����� �Ѿ�� ����

    private int currentIndex = 0; // ���� Ȱ��ȭ�� ������ �ε���
    private bool isCoroutineRunning = false; // �ڷ�ƾ ���� ���� Ȯ��
    public FadeController fadeController;
    public string SceneName; // ���� �� �̸�

    public Button skipButton; // ��ŵ ��ư

    void Start()
    {
        // ��� �������� ó���� ��Ȱ��ȭ
        foreach (GameObject prefab in comicPrefabs)
        {
            prefab.SetActive(false);
        }
        // ù ��° ������ Ȱ��ȭ
        if (comicPrefabs.Length > 0)
        {
            ShowNextPrefab();
        }

        // ��ŵ ��ư�� �޼��� ����
        skipButton.onClick.AddListener(SkipToEnd);
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            // ���� �ڷ�ƾ ����
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
                // ���� ������ ��Ȱ��ȭ
                Destroy(comicPrefabs[currentIndex - 1]);
            }
            // ���� ������ Ȱ��ȭ
            GameObject instance = Instantiate(comicPrefabs[currentIndex], parentTransform);
            instance.SetActive(true);
            currentIndex++;

            // �ڷ�ƾ ����
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
        // ȭ�� ��ȯ ȿ���� ���ְ� �ٷ� �� ��ȯ
        SceneManager.LoadScene(SceneName);
        yield break; // �ڷ�ƾ ����
    }

    // ��ŵ ��ư�� ������ �� ȣ��Ǵ� �޼���
    public void SkipToEnd()
    {
        // �ڷ�ƾ ����
        StopAllCoroutines();

        // ��ŵ ��ư �ı�
        Destroy(skipButton.gameObject);

        // ���� ��� ��ȭ�� �ǳʶٰ� ���� �ٷ� ��ȯ
        StartCoroutine(FadeOutAndLoadNextScene());
    }
}
