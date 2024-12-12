using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public float rotationSpeed = 90f; // 회전 속도 (1초에 90도 회전)
    private float currentRotation = 0f; // 현재 회전 각도 누적 값
    private bool isRotating = true; // 회전 상태를 나타내는 플래그
    private Vector3 startPosition; // 초기 위치
    public float x = -2f; // 이동 거리
    public float y = -2f; // 이동 거리
    private float speed = 1f; // 이동 속도
    public float movespeed = 5f; // 이동 속도
    public float delayTime = 2.0f; // 이동 시작 전 대기 시간

    void Start()
    {
        startPosition = transform.position; // 초기 위치 저장
    }

    void Update()
    {
        if (isRotating)
        {
            // z축을 기준으로 회전값을 변경
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationThisFrame);
            currentRotation += rotationThisFrame;
            Vector3 targetPosition = new Vector3(x, y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            // 현재 회전 각도가 40도 이상이면 회전을 멈춤
            if (currentRotation >= 40f)
            {
                isRotating = false;
                // 정확한 40도로 맞추기 위해 값을 고정
                currentRotation = 40f;
                StartCoroutine(WaitAndMove()); // 각도가 멈추면 이동을 지연시키는 코루틴 시작
            }
        }
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(delayTime); // 지정된 시간 동안 대기

        Vector3 targetPosition = new Vector3(x, y, transform.position.z);

        while (true)
        {
            transform.position += Vector3.left * movespeed * Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }
    }
}