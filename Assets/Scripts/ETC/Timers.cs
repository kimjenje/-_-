using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timers : MonoBehaviour
{
    public Slider timerSlider;
    public float timeLeft; // 남은 시간 (초 단위)
    private bool stopTimer = false;
    private const float fixedDecrement = 1.0f; // `timeLeft` 감소 속도 (초당 고정값)

    // 초기 설정
    void Start()
    {
        timerSlider.maxValue = timeLeft;
        timerSlider.value = timeLeft;

        // 코루틴 시작
        StartCoroutine(DecrementTime());
    }

    // `timeLeft`를 1초마다 `fixedDecrement`만큼 감소시키는 코루틴
    IEnumerator DecrementTime()
    {
        while (!stopTimer)
        {
            // 0.1초 동안 대기
            yield return new WaitForSeconds(0.01f);

            // `timeLeft`를 `fixedDecrement`의 0.1 비율만큼 감소
            timeLeft -= fixedDecrement * 0.01f;

            // `timeLeft`가 0 이하로 떨어졌을 때 타이머를 중지
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                stopTimer = true;
            }

            // 슬라이더의 값을 업데이트
            timerSlider.value = timeLeft;
        }
    }

    // 데미지를 입을 때 `timeLeft`를 줄입니다.
    public void TakeDamage(float amount)
    {
        timeLeft -= amount;

        // `timeLeft`가 0 이하로 떨어졌을 때 게임 오버 처리
        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }

        // 슬라이더의 값을 업데이트합니다.
        timerSlider.value = timeLeft;
    }
}
