using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stab : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;

    public float moveSpeed = 3.0f; // 이동 속도
    public Vector3 targetPosition = new Vector3(-5f, -1.8f, 0f); // 목표 위치
    public Vector3 originalPosition = new Vector3(3f, -1.8f, 0f); // 원래 위치
    private bool movingToTarget = true; // 현재 목표 위치로 이동중인지 여부
    public float timeToWait = 3.0f; // 대기 시간
    private float waitTimer = 0.0f; // 대기 타이머
    private bool hasMoved = false; // 이동이 한 번 발생했는지 여부
    private bool hasStarted = false; // 이동이 시작되었는지 여부
    private float initialWaitTime = 2.0f; // 초기 대기 시간


    void Start()
    {
         _spriteRenderer = GameObject.Find("redbox2").GetComponent<SpriteRenderer>();
        StartCoroutine(redbox());
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

    void Update()
    {
        if (!hasStarted)
        {
            initialWaitTime -= Time.deltaTime;
            if (initialWaitTime <= 0)
            {
                hasStarted = true; // 이동이 시작됨
                waitTimer = 0.0f; // 대기 타이머 초기화
            }
            return;
        }

        if (hasMoved) // 이미 이동한 경우
            return;

        if (movingToTarget)
        {
            // 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 목표 위치에 도달한 경우
            if (transform.position == targetPosition)
            {
                movingToTarget = false; // 원래 위치로 이동하기 위해 상태 변경
                waitTimer = 0.0f; // 대기 타이머 초기화
            }
        }
        else
        {
            // 대기 시간이 지난 경우
            if (waitTimer >= timeToWait)
            {
                // 원래 위치로 이동
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

                // 원래 위치에 도달한 경우
                if (transform.position == originalPosition)
                {
                    hasMoved = true; // 이동 완료 상태 변경
                }
            }
            else
            {
                // 대기 타이머 업데이트
                waitTimer += Time.deltaTime;
            }
        }
    }
}
