using UnityEngine;
using System.Collections;

public class swordup : MonoBehaviour
{
    public float rotationSpeed = 90f; // ȸ�� �ӵ� (1�ʿ� 90�� ȸ��)
    private float currentRotation = 0f; // ���� ȸ�� ���� ���� ��
    private bool isRotating = true; // ȸ�� ���¸� ��Ÿ���� �÷���

    private Vector3 startPosition; // �ʱ� ��ġ
    public float moveDistance = -2f; // �̵� �Ÿ�

    public GameObject objectToActivate; // Ȱ��ȭ�� ������Ʈ

    public float objectMoveDistance = 5f; // ������Ʈ�� �̵��� �Ÿ�
    public float objectMoveSpeed = 2f; // ������Ʈ �̵� �ӵ�

    void Start()
    {
        objectToActivate.SetActive(false);
        startPosition = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        if (isRotating)
        {
            // z���� �������� ȸ������ ����
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationThisFrame);
            currentRotation += rotationThisFrame;

            // �̵� �Ÿ� ���
            float moveFraction = currentRotation / 90f; // ���� ȸ�� ������ ����
            float currentYPosition = Mathf.Lerp(startPosition.y, startPosition.y + moveDistance, moveFraction);
            transform.position = new Vector3(transform.position.x, currentYPosition, transform.position.z);

            // ���� ȸ�� ������ 90�� �̻��̸� ȸ���� ����
            if (currentRotation >= rotationSpeed)
            {
                isRotating = false;
                currentRotation = rotationSpeed; // ��Ȯ�� 90���� ���߱� ���� ���� ����
                transform.position = new Vector3(transform.position.x, startPosition.y + moveDistance, transform.position.z); // ���� ��ġ ����
                ActivateObject();
            }
        }
    }

    void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            StartCoroutine(MoveObjectUpAndDown()); // ������Ʈ �̵� �ڷ�ƾ ����
        }
        else
        {
            Debug.LogWarning("objectToActivate�� �������� �ʾҽ��ϴ�.");
        }
    }

    IEnumerator MoveObjectUpAndDown()
    {
        Vector3 originalPosition = objectToActivate.transform.position;
        Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y + objectMoveDistance, originalPosition.z);

        yield return new WaitForSeconds(2f);

        // ���� �̵�
        while (Vector3.Distance(objectToActivate.transform.position, targetPosition) > 0.01f)
        {
            objectToActivate.transform.position = Vector3.MoveTowards(objectToActivate.transform.position, targetPosition, objectMoveSpeed * Time.deltaTime);
            yield return null;
        }

        // ��� ����

        // ���� ��ġ�� �̵�
        while (Vector3.Distance(objectToActivate.transform.position, originalPosition) > 0.01f)
        {
            objectToActivate.transform.position = Vector3.MoveTowards(objectToActivate.transform.position, originalPosition, objectMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
