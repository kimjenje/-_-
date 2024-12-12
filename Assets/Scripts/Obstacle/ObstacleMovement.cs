using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f; // 이동 속도

    private Rigidbody2D rb; // Rigidbody2D 컴포넌트를 저장할 변수

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 왼쪽 방향으로 속도 설정
        Vector2 movement = new Vector2(-speed, 0f);

        // Rigidbody2D의 속도 설정
        rb.velocity = movement;
    }
}