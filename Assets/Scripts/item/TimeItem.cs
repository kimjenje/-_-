using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : MonoBehaviour
{
    public float timeAmount = 10f; // ������ �ð� (�� ����)
    public Timer timerController; // Ÿ�̸� ��Ʈ�ѷ� ����
    public AudioSource pickupSound;

    private void Start()
    {
        // Ÿ�̸� ��Ʈ�ѷ� ���� ����
        if (timerController == null)
        {
            timerController = FindObjectOfType<Timer>();
            if (timerController == null)
            {
                Debug.LogError("No Timer controller found in the scene.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹���� ��
        {
            if (pickupSound != null)
            {
                pickupSound.Play();
            }

            timerController.AddTime(timeAmount); // Ÿ�̸� ����
            Destroy(gameObject); // ������ ����
        }
    }
}
