using UnityEngine;
using UnityEngine.UI;

public class followobject : MonoBehaviour
{
    public Transform targetObject;  // 움직일 오브젝트
    public Slider slider;           // UI 슬라이더
    public Vector3 offset;          // 오프셋 값 (선택사항)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;  // 메인 카메라 참조
    }

    void Update()
    {
        if (targetObject != null && slider != null)
        {
            // 월드 좌표에서 화면 좌표로 변환
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetObject.position + offset);

            // 슬라이더의 위치를 화면 좌표로 설정
            slider.transform.position = screenPosition;
        }
    }
}
