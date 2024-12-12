using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] float remainingTime;
    public GameObject menuGameover;

    private void Awake()
    {
        menuGameover.SetActive(false);
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            menuGameover.SetActive(true);
            Time.timeScale = 0;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("남은 시간 : [{0:00} : {1:00}]", minutes, seconds);
    }

    public void AddTime(float time)
    {
        remainingTime += time;
    }
}

