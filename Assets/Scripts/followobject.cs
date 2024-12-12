using UnityEngine;
using UnityEngine.UI;

public class followobject : MonoBehaviour
{
    public Transform targetObject;  // ������ ������Ʈ
    public Slider slider;           // UI �����̴�
    public Vector3 offset;          // ������ �� (���û���)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;  // ���� ī�޶� ����
    }

    void Update()
    {
        if (targetObject != null && slider != null)
        {
            // ���� ��ǥ���� ȭ�� ��ǥ�� ��ȯ
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetObject.position + offset);

            // �����̴��� ��ġ�� ȭ�� ��ǥ�� ����
            slider.transform.position = screenPosition;
        }
    }
}
