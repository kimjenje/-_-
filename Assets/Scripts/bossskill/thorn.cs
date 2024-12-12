using UnityEngine;
using System.Collections;

public class thorn : MonoBehaviour
{
    public GameObject[] thorns; // 오브젝트 배열
    private SpriteRenderer _spriteRenderer;

    public float upwardSpeed = 5f; // 위로 올라가는 속도
    public float downwardSpeed = 5f; // 아래로 내려가는 속도
    public float maxHeight = -1f; // 최대 높이
    public float minHeight = -4.5f; // 최소 높이
    public float interval = 0.2f; // 배열 간격
    public float startDelay = 1f; // 시작 딜레이

    void Start()
    {
        thorns = GameObject.FindGameObjectsWithTag("Thorn"); // "Thorn" 태그를 가진 모든 오브젝트를 찾아 배열에 할당
        foreach (GameObject thorn in thorns)
        {
            thorn.SetActive(true);
        }
        _spriteRenderer = GameObject.Find("redbox1").GetComponent<SpriteRenderer>();
        StartCoroutine(redbox());
        // 1초 후에 이동 코루틴 시작
        Invoke("StartMoving", startDelay);
    }

    void StartMoving()
    {
        StartCoroutine(MoveThorns()); // 이동 코루틴 시작
    }
    
    
    IEnumerator redbox()
    {
        for(int i=0; i<3; i++)
        {
            _spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = new Color(1, 0, 0, 0);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator MoveThorns()
    {
        // 각 오브젝트에 대해 순차적으로 이동
        for (int i = 0; i < thorns.Length; i++)
        {
            StartCoroutine(Move(thorns[i])); // 이동 코루틴 시작
            yield return new WaitForSeconds(interval); // interval만큼 대기
        }
    }

    IEnumerator Move(GameObject thorn)
    {
        bool isMovingUp = true; // 초기에 위로 움직임

        while (true)
        {
            if (isMovingUp)
            {
                thorn.transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime);
                if (thorn.transform.position.y >= maxHeight)
                {
                    isMovingUp = false; // 아래로 이동 시작
                }
            }
            else
            {
                thorn.transform.Translate(Vector3.down * downwardSpeed * Time.deltaTime);
                if (thorn.transform.position.y <= minHeight)
                {
                    isMovingUp = true; // 위로 이동 시작
                    thorn.SetActive(false); // 이동 후 비활성화
                    yield break; // 코루틴 종료
                }
            }

            yield return null;
        }
    }
}
