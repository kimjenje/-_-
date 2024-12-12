using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager_ex : MonoBehaviour
{
    public float scorePerSecond = 10f; // 1초당 획득할 점수
    private float score = 0f;

    public GameObject[] Item;
    public float Coin;
    public float AttackUp;
    public float HealthItem;
    public float SpeedItem;

    public float scoreToAdd;

    void Update()
    {
        // 시간이 흐를 때마다 점수 증가
        score += scorePerSecond * Time.deltaTime;
    }

    // 현재 점수를 반환하는 함수
    public float GetScore()
    {
        return score;
    }

    // 아이템을 먹으면 점수를 추가하는 함수
    public void AddScore(float amount)
    {
        score += amount;
    }

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "AttackUp":
            case "Coin":
            case "SpeedItem":
            case "HealthItem":
            
            
                if(collision.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
                ScoreManager_ex scoreManager = FindObjectOfType<ScoreManager_ex>();
                scoreManager.AddScore(scoreToAdd);
                scoreToAdd += AttackUp;
                scoreToAdd += Coin;
                scoreToAdd += SpeedItem;
                scoreToAdd += HealthItem;
            }
            
            
                break;
        }  
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        switch (collision.tag)
        {
            case "AttackUp":
            case "Coin":
            case "SpeedItem":
            case "HealthItem":

                ScoreManager_ex scoreManager = FindObjectOfType<ScoreManager_ex>();
                if (scoreManager != null)
                {
                    
                scoreManager.AddScore(scoreToAdd);
                scoreToAdd += AttackUp;
                scoreToAdd += Coin;
                scoreToAdd += SpeedItem;
                scoreToAdd += HealthItem;
                Destroy(collision.gameObject);
                
                }
                else
                {
                    Debug.LogError("ScoreManager_ex script not found.");
                }
                break;
        }
    }
}
}
