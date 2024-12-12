using UnityEngine;
using System.Collections;

public class swordup : MonoBehaviour
{
    public float rotationSpeed = 90f; // 회전 속도 (1초에 90도 회전)
    private float currentRotation = 0f; // 현재 회전 각도 누적 값
    private bool isRotating = true; // 회전 상태를 나타내는 플래그

    private Vector3 startPosition; // 초기 위치
    public float moveDistance = -2f; // 이동 거리

    public GameObject objectToActivate; // 활성화할 오브젝트

    public float objectMoveDistance = 5f; // 오브젝트가 이동할 거리
    public float objectMoveSpeed = 2f; // 오브젝트 이동 속도

    void Start()
    {
        objectToActivate.SetActive(false);
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

            // 이동 거리 계산
            float moveFraction = currentRotation / 90f; // 현재 회전 각도의 비율
            float currentYPosition = Mathf.Lerp(startPosition.y, startPosition.y + moveDistance, moveFraction);
            transform.position = new Vector3(transform.position.x, currentYPosition, transform.position.z);

            // 현재 회전 각도가 90도 이상이면 회전을 멈춤
            if (currentRotation >= rotationSpeed)
            {
                isRotating = false;
                currentRotation = rotationSpeed; // 정확한 90도로 맞추기 위해 값을 고정
                transform.position = new Vector3(transform.position.x, startPosition.y + moveDistance, transform.position.z); // 최종 위치 설정
                ActivateObject();
            }
        }
    }

    void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            StartCoroutine(MoveObjectUpAndDown()); // 오브젝트 이동 코루틴 시작
        }
        else
        {
            Debug.LogWarning("objectToActivate가 설정되지 않았습니다.");
        }
    }

    IEnumerator MoveObjectUpAndDown()
    {
        Vector3 originalPosition = objectToActivate.transform.position;
        Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y + objectMoveDistance, originalPosition.z);

        yield return new WaitForSeconds(2f);

        // 위로 이동
        while (Vector3.Distance(objectToActivate.transform.position, targetPosition) > 0.01f)
        {
            objectToActivate.transform.position = Vector3.MoveTowards(objectToActivate.transform.position, targetPosition, objectMoveSpeed * Time.deltaTime);
            yield return null;
        }

        // 잠시 멈춤

        // 원래 위치로 이동
        while (Vector3.Distance(objectToActivate.transform.position, originalPosition) > 0.01f)
        {
            objectToActivate.transform.position = Vector3.MoveTowards(objectToActivate.transform.position, originalPosition, objectMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
