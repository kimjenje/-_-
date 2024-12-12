using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : MonoBehaviour
{
    public float timeAmount = 10f; // 증가할 시간 (초 단위)
    public Timer timerController; // 타이머 컨트롤러 참조
    public AudioSource pickupSound;

    private void Start()
    {
        // 타이머 컨트롤러 참조 설정
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
        if (other.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            if (pickupSound != null)
            {
                pickupSound.Play();
            }

            timerController.AddTime(timeAmount); // 타이머 증가
            Destroy(gameObject); // 아이템 제거
        }
    }
}
