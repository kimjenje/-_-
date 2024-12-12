using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAttack : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도
    public float startX = 6.0f; // 시작 x 위치
    public float endX = -14.0f; // 도달해야 하는 x 위치
    public float returnSpeed = 5.0f; // 돌아가는 속도
    public float originalY = 0.5f; // 원래 y 위치

    private Vector3 targetPosition;
    private Vector3 originalPosition;
    private bool hasStarted = false;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        // 초기 대기 시간 설정
        _spriteRenderer = GameObject.Find("redbox4").GetComponent<SpriteRenderer>();
        StartCoroutine(redbox());

        StartCoroutine(InitialDelay());

        // 랜덤으로 y 값을 설정 (-2 또는 0.5)
        float randomY = Random.value < 0.5f ? -2.0f : -0.8f;
        transform.position = new Vector3(startX, randomY, 0);
        targetPosition = new Vector3(endX, randomY, 0);
        originalPosition = new Vector3(startX, originalY, 0);
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

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(2.0f); // 초기 대기 시간 (2초)
        hasStarted = true; // 시작 신호
    }

    void Update()
    {
        if (hasStarted) // 시작 신호가 왔을 때만 실행
        {
            // 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // 목표 위치에 도달하면 다시 시작 위치로 이동
            if (transform.position.x <= endX)
            {
                targetPosition = new Vector3(startX, transform.position.y, 0); // 시작 위치로 설정
                StartCoroutine(ReturnToStart()); // 시작 위치로 돌아가는 코루틴 실행
            }

            // 원래 위치로 이동
            if (transform.position == originalPosition)
            {
                // 멈춰있으면 원래 위치로 이동
                targetPosition = originalPosition;
            }
        }
    }

    IEnumerator ReturnToStart()
    {
        while (transform.position.x < startX)
        {
            transform.position += Vector3.right * returnSpeed * Time.deltaTime;
            yield return null;
        }
        // 돌아온 후 원래 위치로 이동
        targetPosition = originalPosition;
    }
}
