using UnityEngine;

public class swordarm : MonoBehaviour
{
    public float rotationSpeed = 90f; // ȸ�� �ӵ� (1�ʿ� 90�� ȸ��)
    private float currentRotation = 0f; // ���� ȸ�� ���� ���� ��
    private bool isRotating = true; // ȸ�� ���¸� ��Ÿ���� �÷���

    private Vector3 startPosition; // �ʱ� ��ġ
    public float moveDistance = -2f; // �̵� �Ÿ�

    public GameObject objectToActivate; // Ȱ��ȭ�� ������Ʈ

    void Start()
    {
        startPosition = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        if (isRotating)
        {
            // z���� �������� ���������� ȸ������ ���� (���� ����)
            float rotationThisFrame = -rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationThisFrame);
            currentRotation += Mathf.Abs(rotationThisFrame);

            // �̵� �Ÿ� ���
            float moveFraction = currentRotation / 90f; // ���� ȸ�� ������ ����
            float currentYPosition = Mathf.Lerp(startPosition.y, startPosition.y + moveDistance, moveFraction);
            transform.position = new Vector3(transform.position.x, currentYPosition, transform.position.z);

            if (currentRotation >= 45f)
            {
                // ��Ȱ��ȭ�� ������Ʈ Ȱ��ȭ
                ActivateObject();
            }
            // ���� ȸ�� ������ 90�� �̻��̸� ȸ���� ����
            if (currentRotation >= 30f)
            {
                isRotating = false;
                currentRotation = 90f; // ��Ȯ�� 90���� ���߱� ���� ���� ����
                transform.position = new Vector3(transform.position.x, startPosition.y + moveDistance, transform.position.z); // ���� ��ġ ����
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
            Debug.LogWarning("objectToActivate�� �������� �ʾҽ��ϴ�.");
        }
    }
}
