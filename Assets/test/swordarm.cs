using UnityEngine;

public class swordarm : MonoBehaviour
{
    public float rotationSpeed = 90f; // 회전 속도 (1초에 90도 회전)
    private float currentRotation = 0f; // 현재 회전 각도 누적 값
    private bool isRotating = true; // 회전 상태를 나타내는 플래그

    private Vector3 startPosition; // 초기 위치
    public float moveDistance = -2f; // 이동 거리

    public GameObject objectToActivate; // 활성화할 오브젝트

    void Start()
    {
        startPosition = transform.position; // 초기 위치 저장
    }

    void Update()
    {
        if (isRotating)
        {
            // z축을 기준으로 오른쪽으로 회전값을 변경 (음수 방향)
            float rotationThisFrame = -rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationThisFrame);
            currentRotation += Mathf.Abs(rotationThisFrame);

            // 이동 거리 계산
            float moveFraction = currentRotation / 90f; // 현재 회전 각도의 비율
            float currentYPosition = Mathf.Lerp(startPosition.y, startPosition.y + moveDistance, moveFraction);
            transform.position = new Vector3(transform.position.x, currentYPosition, transform.position.z);

            if (currentRotation >= 45f)
            {
                // 비활성화된 오브젝트 활성화
                ActivateObject();
            }
            // 현재 회전 각도가 90도 이상이면 회전을 멈춤
            if (currentRotation >= 30f)
            {
                isRotating = false;
                currentRotation = 90f; // 정확한 90도로 맞추기 위해 값을 고정
                transform.position = new Vector3(transform.position.x, startPosition.y + moveDistance, transform.position.z); // 최종 위치 설정
            }
        }
    }

    void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("objectToActivate가 설정되지 않았습니다.");
        }
    }
}
